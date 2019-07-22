using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Net;
using System.Net.Sockets;
using System.Text;

public class NetworkClient : MonoBehaviour
{
    static Socket socket;
    IPEndPoint ep;
    string serverIP = "127.0.0.1";
    int serverPort = 9876;
    //byte[] sendBuff = new byte[1024];
    byte[] sendBuff = Encoding.UTF8.GetBytes("HIHI!!");
    byte[] recvBuff = new byte[1024];

    void Start()
    {
        ConnectSocket();
    }

    void Update()
    {
        socket.Send(sendBuff, SocketFlags.None);

        int n = socket.Receive(recvBuff);
        print("Recv byte = " + n);
        print(Encoding.UTF8.GetString(recvBuff, 0, n));
    }

    public bool ConnectSocket()
    {
        try
        {
            socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            ep = new IPEndPoint(IPAddress.Parse(serverIP), serverPort);
            socket.Connect(ep);
        }
        catch
        {
            return false;
        }
        return true;
    }
}
