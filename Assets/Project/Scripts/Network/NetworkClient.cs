using UnityEngine;
using UnityEngine.SceneManagement;

using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Text;
using System.Collections.Generic;

public class NetworkClient : MonoBehaviour
{
    private static ManualResetEvent connectDone = new ManualResetEvent(false);
    private static ManualResetEvent sendDone = new ManualResetEvent(false);
    private static ManualResetEvent receiveDone = new ManualResetEvent(false);

    private static Queue<NetworkPacket> sendingPacketQueue = new Queue<NetworkPacket>();
    private static Queue<NetworkPacket> receivingPacketQueue = new Queue<NetworkPacket>();

    private static NetworkClient instance = null;
    public static NetworkClient Instance
    {
        get
        {
            if (!instance)
            {
                instance = FindObjectOfType(typeof(NetworkClient)) as NetworkClient;
                if (!instance)
                {
                    Debug.Log("ERROR : NO SceneManager");
                }
            }
            return instance;
        }
    }

    public static void PacketEnqueue(NetworkPacket packet)
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
            Debug.Log(e.ToString());
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
            Debug.Log(e.ToString());
        }
    }

    private static void ReceiveCallback(IAsyncResult ar)
    {
        try
        {
            StateObject state = (StateObject)ar.AsyncState;
            Socket client = state.workSocket;

            int bytesRead = client.EndReceive(ar);

            if (bytesRead > 0)
            {
                Debug.Log("READ : " + bytesRead);
                for (int i = 0; i < bytesRead; i++)
                    state.packetBuilder.Enqueue(state.buffer[i]);

                while (state.packetBuilder.Peek() <= state.packetBuilder.Count)
                {
                    byte[] tmpPacketData = new byte[state.packetBuilder.Peek()];

                    for (int i = 0; i < state.packetBuilder.Peek(); i++)
                        tmpPacketData[i] = state.packetBuilder.Dequeue();

                    NetworkPacket packet = new NetworkPacket(tmpPacketData);

                    receivingPacketQueue.Enqueue(packet);
                }
            }

            client.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0,
                                new AsyncCallback(ReceiveCallback), state);
        }
        catch (Exception e)
        {
            Debug.Log(e.ToString());
        }
    }


    private static void Send(Socket client)
    {
        if (sendingPacketQueue.Count > 0)
        {
            NetworkPacket packet = sendingPacketQueue.Dequeue();
            byte[] byteData = packet.GetBytes();
            packet.Print();

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
            Debug.Log("Sent {0} bytes to server." + bytesSent);

            sendDone.Set();
        }
        catch (Exception e)
        {
            Debug.Log(e.ToString());
        }
    }

    static void GetDataFromPacket(NetworkPacket packet)
    {
        Debug.Log(packet.packetType);

        switch (packet.packetType)
        {
            case PacketType.OPPONENT_SOCKET:
                TeamColor teamColor = packet.packetData[0] == (byte)PacketType.TEAM_BLACK ?
                                                TeamColor.Black : TeamColor.White;

                PlayerManager.SetPlayerTeamColor(teamColor);
                SceneManager.NewScene("Playing");
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

    public void StartClient()
    {
        new Thread(new ThreadStart(Connect)).Start();
    }

    private void Update()
    {
        if (receivingPacketQueue.Count > 0)
        {
            Debug.Log("B");
            NetworkPacket packet = receivingPacketQueue.Dequeue();
            Debug.Log(packet.ToString());
            GetDataFromPacket(packet);
        }
    }
}
