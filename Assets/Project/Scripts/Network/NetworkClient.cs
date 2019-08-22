using UnityEngine;
using UnityEngine.SceneManagement;

using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Text;
using System.Linq;

public class NetworkClient : MonoBehaviour
{

    private static ManualResetEvent connectDone = new ManualResetEvent(false);
    private static ManualResetEvent sendDone = new ManualResetEvent(false);
    private static ManualResetEvent receiveDone = new ManualResetEvent(false);

    private static String response = String.Empty;

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

            //Send(client, "This is a test<EOF>");
            //sendDone.WaitOne();

            Receive(client);
            receiveDone.WaitOne();

            Debug.Log("Response received : " + response);

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
                state.packetBuilder.AddRange(state.buffer);

                int idx = state.packetBuilder.FindIndex(x => x == (byte)PacketType.ETX);
                while(idx != -1)
                {
                    byte[] packetData = state.packetBuilder.GetRange(1, idx-1).ToArray();
                    NetworkPacket packet = new NetworkPacket((PacketType)state.packetBuilder[0], packetData);
                    state.packetQueue.Enqueue(packet);
                    state.packetBuilder.RemoveRange(0, idx + 1);

                    idx = state.packetBuilder.FindIndex(x => x == (byte)PacketType.ETX);
                }

            }

            while(state.packetQueue.Count > 0)
            {
                NetworkPacket packet = state.packetQueue.Dequeue();
                GetDataFromPacket(packet);
            }

            client.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0,
                                new AsyncCallback(ReceiveCallback), state);
            //receiveDone.Set();
        }
        catch (Exception e)
        {
            Debug.Log(e.ToString());
        }
    }

    private static void Send(Socket client, String data)
    {
        byte[] byteData = Encoding.ASCII.GetBytes(data);

        client.BeginSend(byteData, 0, byteData.Length, 0,
                        new AsyncCallback(SendCallback), client);
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
        Debug.Log(packet.ToString());
        packet.Print();
        switch (packet.packetType)
        {
            case PacketType.OPPONENT_SOCKET:
                TeamColor teamColor = packet.packetBytes[1] == (byte)PacketType.TEAM_BLACK ?
                                                TeamColor.Black : TeamColor.White;

                PlayerManager.SetPlayerTeamColor(teamColor);
                SceneManager.NewScene("Playing");
                break;

            case PacketType.MOVE:

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
}