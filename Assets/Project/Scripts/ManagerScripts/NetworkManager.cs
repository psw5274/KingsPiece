using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkManager : SingletonPattern<NetworkManager>
{
    private static ManualResetEvent connectDone = new ManualResetEvent(false);
    private static ManualResetEvent sendDone = new ManualResetEvent(false);
    private static ManualResetEvent receiveDone = new ManualResetEvent(false);

    private static Queue<NetworkPacket> sendingPacketQueue = new Queue<NetworkPacket>();
    private static Queue<NetworkPacket> receivingPacketQueue = new Queue<NetworkPacket>();



    public static void SendingPacketEnqueue(NetworkPacket packet)
    {
        sendingPacketQueue.Enqueue(packet);
    }

    private static void Connect()
    {
        try
        {
            IPEndPoint remoteEP = Net.IEP;

            Socket client = new Socket(Net.IEP.AddressFamily,
                                        SocketType.Stream, ProtocolType.Tcp);

            client.BeginConnect(remoteEP,
                                new AsyncCallback(ConnectCallback), client);

            connectDone.WaitOne();

            Receive(client);

            while (true)
            {
                //sendDone.Reset();
                Send(client);
                sendDone.WaitOne();
            }
            //client.Shutdown(SocketShutdown.Both);
            //client.Close();

        }
        catch (Exception e)
        {
            Console.WriteLine(e.ToString());
        }
    }

    private static void ConnectCallback(IAsyncResult ar)
    {
        try
        {
            Socket client = (Socket)ar.AsyncState;

            client.EndConnect(ar);

            Console.WriteLine("Socket connected to {0}",
                              client.RemoteEndPoint.ToString());

            connectDone.Set();
        }
        catch (Exception e)
        {
            Console.WriteLine(e.ToString());
        }
    }

    private static void Receive(Socket client)
    {
        try
        {
            StateObject state = new StateObject();
            state.workSocket = client;

            client.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0,
                                new AsyncCallback(ReceiveCallback), state);
        }
        catch (Exception e)
        {
            Console.WriteLine(e.ToString());
        }
    }

    private static void ReceiveCallback(IAsyncResult ar)
    {
        StateObject state = (StateObject)ar.AsyncState;
        Socket client = state.workSocket;
        int bytesRead = 0;

        try
        {
            bytesRead = client.EndReceive(ar);
        }
        catch (SocketException e)
        {
            if (e.ErrorCode == 10054)
            {
                Console.WriteLine("Socket Disconnected");
            }

            else
            {
                Console.WriteLine("Socket Exception : {0}", e.ErrorCode);
            }
            return;
        }

        if (bytesRead > 0)
        {
            Console.WriteLine("RECV : " + bytesRead);
            for (int i = 0; i < bytesRead; i++)
                state.packetBuilder.Enqueue(state.buffer[i]);

            byte packetSize = state.packetBuilder.Peek();
            while (packetSize <= state.packetBuilder.Count)
            {
                byte[] tmpPacketData = new byte[state.packetBuilder.Peek()];

                for (int i = 0; i < packetSize; i++)
                    tmpPacketData[i] = state.packetBuilder.Dequeue();

                NetworkPacket packet = new NetworkPacket(tmpPacketData);

                receivingPacketQueue.Enqueue(packet);
            }
        }

        client.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0,
                            new AsyncCallback(ReceiveCallback), state);
    }


    private static void Send(Socket client)
    {
        if (sendingPacketQueue.Count > 0)
        {
            NetworkPacket packet = sendingPacketQueue.Dequeue();
            byte[] byteData = packet.GetBytes();

            client.BeginSend(byteData, 0, byteData.Length, 0,
                            new AsyncCallback(SendCallback), client);
        }
        sendDone.Set();
    }

    private static void SendCallback(IAsyncResult ar)
    {
        try
        {
            Socket client = (Socket)ar.AsyncState;

            int bytesSent = client.EndSend(ar);
            Console.WriteLine("Sent {0} bytes to server." + bytesSent);

            sendDone.Set();
        }
        catch (Exception e)
        {
            Console.WriteLine(e.ToString());
        }
    }

    public void StartClient()
    {
        new Thread(new ThreadStart(Connect)).Start();
    }

    private static void Update()
    {
        if (receivingPacketQueue.Count > 0)
        {
            NetworkPacket packet = receivingPacketQueue.Dequeue();
            Console.WriteLine(packet);
            Console.WriteLine(packet.ToString());
        }
    }
}
