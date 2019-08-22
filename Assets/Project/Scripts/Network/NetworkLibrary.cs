
using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum PacketType : byte
{
    STX = 0x02,
    ETX = 0x03,
    OPPONENT_SOCKET,

    TEAM_WHITE,
    TEAM_BLACK,

    MOVE,   // |x|y|to_x|to_y|
    ATTACK, // |x|y|to_x|to_y|num|
    SKILL_ATK_ADDITION,
    SKILL_ATK_MULTIPLICATION,
    SKILL_HP_ADDITION,
    SKILL_HP_DAMAGE,
    SKILL_HP_HEAL,
    SKILL_HP_MULTIPLICATION,
    SKILL_IMMOVABLE,
    SKILL_UNBEATABLE,
}

public struct NetworkPacket
{
    public PacketType packetType;
    public byte[] packetBytes;

    public NetworkPacket(PacketType packetType, params byte[] data)
    {
        this.packetType = packetType;

        packetBytes = new byte[2 + data.Length];

        packetBytes[0] = (byte)packetType;

        int i = 0;
        for (; i < data.Length; i++)
        {
            packetBytes[i + 1] = data[i];
        }

        packetBytes[i+1] = (byte)PacketType.ETX;
    }

    public byte[] GetBytes()
    {
        return packetBytes;
    }

    public override string ToString()
    {
        return Encoding.UTF8.GetString(packetBytes);
    }

    public void Print()
    {
        string str = "";
        for (int i = 0; i < packetBytes.Length; i++)
        {
            str += packetBytes[i].ToString();
            str += " ";
        }
        Debug.Log(str);
    }
}


public static class Net
{
    public static string serverIP = "127.0.0.1";
    public static int serverPort = 9876;
    
    static IPHostEntry ipHostInfo = Dns.GetHostEntry(serverIP);
    static IPAddress ipAddress = ipHostInfo.AddressList[0];

    public static IPEndPoint IEP = new IPEndPoint(ipAddress, serverPort);

}


public class StateObject
{
    public Socket workSocket = null;
    public const int BufferSize = 1024;
    public byte[] buffer = new byte[BufferSize];
    public StringBuilder sb = new StringBuilder();
    
    public List<byte> packetBuilder = new List<byte>();
    public Queue<NetworkPacket> packetQueue = new Queue<NetworkPacket>();
}
