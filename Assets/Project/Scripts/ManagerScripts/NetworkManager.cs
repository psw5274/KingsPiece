using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NetworkManager : Manager<NetworkManager>
{
    private static ManualResetEvent connectDone = new ManualResetEvent(false);
    private static ManualResetEvent sendDone = new ManualResetEvent(false);
    //private static ManualResetEvent receiveDone = new ManualResetEvent(false);

    // Send Packet From Client to Server
    private static Queue<NetworkPacket> sendingPacketQueue = new Queue<NetworkPacket>();
    // Receieve Packet From Server to Client
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
        Socket client = (Socket)ar.AsyncState;

        client.EndConnect(ar);
        connectDone.Set();

        Debug.Log("Socket connected to " + client.RemoteEndPoint.ToString());
        try
        {

        }
        catch (Exception e)
        {
            Debug.Log(e.ToString());
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
                Debug.Log("Socket Disconnected");
            }

            else
            {
                Debug.Log("Socket Exception : " + e.ErrorCode);
            }
            return;
        }

        if (bytesRead > 0)
        {
            Debug.Log("RECV : " + bytesRead);
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
            Debug.Log("Sent " + bytesSent + " bytes to server.");

            sendDone.Set();
        }
        catch (Exception e)
        {
            Debug.Log(e.ToString());
        }
    }

    public void StartClient()
    {
        new Thread(new ThreadStart(Connect)).Start();
    }
    void GetDataFromPacket(NetworkPacket packet)
    {
        Debug.Log(packet.packetType);

        switch (packet.packetType)
        {
            case PacketType.OPPONENT_SOCKET:
                TeamColor teamColor = packet.packetData[0] == (byte)PacketType.TEAM_BLACK ?
                                                TeamColor.Black : TeamColor.White;

                PlayerManager.Instance.SetPlayerTeamColor(teamColor);
                SceneManager.LoadScene("Playing");
                break;

            case PacketType.MOVE:
                int x, y, target_x, target_y;

                x = packet.packetData[0];
                y = packet.packetData[1];

                target_x = packet.packetData[2];
                target_y = packet.packetData[3];

                Piece selectedPiece = BoardManager.Instance.boardStatus[x][y].GetComponent<Piece>();

                selectedPiece.Move(new BoardCoord(target_x, target_y));
                Debug.Log("MOVE MOVE");
                break;

            case PacketType.ATTACK:

                break;

            default:
                break;
        }
    }

    private void Update()
    {
        if (receivingPacketQueue.Count > 0)
        {
            NetworkPacket packet = receivingPacketQueue.Dequeue();
            Debug.Log(packet);
            Debug.Log(packet.ToString());

            GetDataFromPacket(packet);
        }
    }
}
