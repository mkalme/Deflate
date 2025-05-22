namespace Deflate
{
    public class StoreBlock : IDeflateBlock
    {
        public int Decompress(ref BitReadOnlyStream input, IWriter output)
        {
            input.AdvanceToNearestFullByte();

            ushort length = input.ReadUInt16();
            input.IncrementPositionInsideByte(16);

            output.Write(input.Buffer, input.Position, length);
            input.Position += length;

            return length;
        }
    }
}
