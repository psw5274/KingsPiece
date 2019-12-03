using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Collections;
using System.Collections.Generic;

public enum PacketType : byte
{
    OPPONENT_SOCKET,

    TEAM_WHITE,
    TEAM_BLACK,

    MOVE,   // |x|y|targ_x|targ_y|
    ATTACK, // |x|y|targ_x|targ_y|
    MAGIC,  // |targ_x|targ_y|card_name|
    SKILL_ATK_ADDITION,
    SKILL_ATK_MULTIPLICATION,
    SKILL_HP_ADDITION,
    SKILL_HP_DAMAGE,
    SKILL_HP_HEAL,
    SKILL_HP_MULTIPLICATION,
    SKILL_IMMOVABLE,
    SKILL_UNBEATABLE,
    TURN_END,
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

    /// <summary>
    /// NetworkPacket Constructor with PacketType.Move
    /// </summary>
    /// <param name="packetType"></param>
    /// <param name="originX"></param>
    /// <param name="originY"></param>
    /// <param name="destX"></param>
    /// <param name="destY"></param>
    public NetworkPacket(PacketType packetType, int originX, int originY, int destX, int destY)
    {
        this.packetType = PacketType.MOVE;
        this.packetData = new byte[4];

        packetData[0] = (byte)originX;
        packetData[1] = (byte)originY;
        packetData[2] = (byte)destX;
        packetData[3] = (byte)destY;

        this.packetLength = (byte)(packetData.Length + 2);
    }

    /// <summary>
    /// NetworkPacket Constructor with PacketType.MAGINC
    /// </summary>
    public NetworkPacket(PacketType packetType,int targetX, int targetY, string cardName)
    {
        this.packetType = PacketType.MAGIC;
        this.packetData = new byte[2 + cardName.Length];

        packetData[0] = (byte)targetX;
        packetData[1] = (byte)targetY;

        for(int i = 0; i < cardName.Length; i++)
        {
            packetData[i + 2] = (byte)cardName[i];
        }

        this.packetLength = (byte)(packetData.Length + 2);
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
}


public static class Net
{
    public static string serverIP = "127.0.0.1";
    public static int serverPort = 1234;

    static IPHostEntry ipHostInfo = Dns.GetHostEntry(serverIP);
    //static IPAddress ipAddress = ipHostInfo.AddressList[0];
    static IPAddress ipAddress = IPAddress.Parse(serverIP);

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
