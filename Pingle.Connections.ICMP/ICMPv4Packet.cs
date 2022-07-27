namespace Pingle.Connections.ICMP;

public class ICMPv4Packet : PayloadContainer
{
    public byte Type;
    public byte SubCode;
    public ushort Checksum;
    public ushort Identifier;
    public ushort SequenceNumber;

    private static readonly short ICMPHeaderLength = 8;
    private byte[] data;

    public ICMPv4Packet(int payloadLength = 32)
    {
        data = BuildPayload(payloadLength);
    }

    public byte[] Payload => data;

    public static ICMPv4Packet Parse(byte[] sourceData, ref int bytesRead)
    {
        // todo: parse packet
        throw new NotImplementedException();
    }

    private byte[] BuildPayload(int payloadLength)
    {
        var payload = new byte[payloadLength];
        new Random().NextBytes(payload);

        var data = new byte[ICMPHeaderLength + sizeof(ushort) * 2 + payloadLength];
        var offset = 0;
        data[offset++] = 8; // 0 - type
        data[offset++] = 0; // 1 - code
        data[offset++] = 0; // 2 - checksum(0)
        data[offset++] = 0; // 3 - checksum(1)
        var id = BitConverter.GetBytes((ushort)0);

        Array.Copy(id, 0, data, offset, id.Length);
        offset += id.Length;

        var sequence = BitConverter.GetBytes((ushort)0);
        Array.Copy(sequence, 0, data, offset, sequence.Length);
        offset += sequence.Length;

        Array.Copy(payload, 0, data, offset, payloadLength);

        var checksum = BitConverter.GetBytes(CheckSum(data));
        Array.Copy(checksum, 0, data, 2, checksum.Length);

        return data;
    }

    private ushort CheckSum(byte[] payload)
    {
        uint checkSum = 0;

        var packetSize = payload.Length;
        var offset = 0;

        while (offset < packetSize)
        {
            checkSum += Convert.ToUInt32(BitConverter.ToUInt16(payload, offset));
            offset += 2;
        }

        checkSum = (checkSum >> 16) + (checkSum & 0xffff);
        checkSum += checkSum >> 16;

        return (ushort)~checkSum;
    }
}
