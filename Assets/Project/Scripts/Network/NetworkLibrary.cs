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
    // Packet's length. count self(packetLength variable)
    public byte packetLength;
    public PacketType packetType;
    public byte[] packetData;

    public NetworkPacket(params byte[] data)
    {
        packetLength = data[0];
        packetType = (PacketType)data[1];

        packetData = new byte[packetLength - 2];

        for (int i = 2; i < data.Length; i++)
            packetData[i - 2] = data[i];
    }

    public NetworkPacket(PacketType packetType, params byte[] data)
    {
        this.packetType = packetType;
        this.packetData = (byte[])data.Clone();
        this.packetLength = (byte)(data.Length + 2);
    }

    public byte[] GetBytes()
    {
        byte[] retByte = new byte[this.packetLength];
        retByte[0] = packetLength;
        retByte[1] = (byte)packetType;
        packetData.CopyTo(retByte, 2);

        return retByte;
    }

    public override string ToString()
    {
        return Encoding.ASCII.GetString(GetBytes());
    }

    public void Print()
    {
        string str = "";
        for (int i = 0; i < packetData.Length; i++)
        {
            str += packetData[i].ToString();
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

    public Queue<byte> packetBuilder = new Queue<byte>();
    public Queue<NetworkPacket> packetQueue = new Queue<NetworkPacket>();
}
