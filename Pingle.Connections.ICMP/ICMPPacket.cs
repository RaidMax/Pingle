using System.Net;
using System.Net.Sockets;

namespace Pingle.Connections.ICMP;

public class ICMPPacket : PayloadContainer
{
    public readonly byte Type;
    public readonly byte SubCode;
    public readonly ushort Checksum;
    public readonly ushort Identifier;
    public readonly ushort SequenceNumber;
    public readonly byte[] Payload;

    private static readonly short ICMPHeaderLength = 8;
    private static uint _nextSequence = 0;
    private static readonly object LockObject = new();

    public ICMPPacket(int payloadLength = 32, AddressFamily addressFamily = AddressFamily.InterNetwork)
    {
        if (_nextSequence == ushort.MaxValue)
        {
            lock (LockObject)
            {
                _nextSequence = 0;
            }
        }
        else
        {
            Interlocked.Increment(ref _nextSequence);
        }

        Type = (byte)(addressFamily == AddressFamily.InterNetwork ? 8 : 128);
        Payload = BuildPayload(payloadLength);
        Identifier = (ushort)_nextSequence;
        SequenceNumber = Identifier;
    }

    private ICMPPacket(byte type, byte subCode, ushort checksum, ushort identifier, ushort sequenceNumber, byte[] payload)
    {
        Type = type;
        SubCode = subCode;
        Checksum = checksum;
        Identifier = identifier;
        SequenceNumber = sequenceNumber;
        Payload = payload;
    }

    public byte[] ToByteArray()
    {
        var buffer = new byte[ICMPHeaderLength + sizeof(ushort) * 2 + Payload.Length];
        var offset = 0;
        buffer[offset++] = Type;
        buffer[offset++] = SubCode;
        buffer[offset++] = 0;
        buffer[offset++] = 0;

        var id = BitConverter.GetBytes(Identifier).Reverse().ToArray();
        Array.Copy(id, 0, buffer, offset, id.Length);
        offset += id.Length;

        var sequence = BitConverter.GetBytes(SequenceNumber).Reverse().ToArray();
        Array.Copy(sequence, 0, buffer, offset, sequence.Length);
        offset += sequence.Length;

        Array.Copy(Payload, 0, buffer, offset, Payload.Length);

        var checksum = BitConverter.GetBytes(GenerateChecksum(buffer));
        Array.Copy(checksum, 0, buffer, 2, checksum.Length);
        
        return buffer;
    }

    public static ICMPPacket Parse(byte[] sourceData)
    {
        if (sourceData.Length < ICMPHeaderLength)
        {
            throw new ArgumentException("Source data is shorter than header length", nameof(sourceData));
        }

        var offset = 0;
        var type = sourceData[offset++];
        var code = sourceData[offset++];
        var checksum = BitConverter.ToUInt16(sourceData, offset);
        offset += 2;
        var id = BitConverter.ToUInt16(sourceData, offset);
        offset += 2;
        var sequence = BitConverter.ToUInt16(sourceData, offset);
        offset += 2;
        var payload = new byte[sourceData.Length - offset];
        Array.Copy(sourceData, offset, payload, 0, sourceData.Length - offset);
        return new ICMPPacket(type, code, checksum, id, sequence, payload);
    }

    private static byte[] BuildPayload(int payloadLength)
    {
        var payload = new byte[payloadLength];
        new Random().NextBytes(payload);
        return payload;
    }

    private ushort GenerateChecksum(byte[] buffer)
    {
        uint checkSum = 0;

        var packetSize = buffer.Length;
        var offset = 0;

        while (offset < packetSize)
        {
            checkSum += Convert.ToUInt32(BitConverter.ToUInt16(buffer, offset));
            offset += 2;
        }

        checkSum = (checkSum >> 16) + (checkSum & 0xffff);
        checkSum += checkSum >> 16;

        return (ushort)~checkSum;
    }
}
