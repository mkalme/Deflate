namespace Deflate
{
    public class ArrayWriter : IWriter
    {
        public byte[] Buffer { get; set; }
        public int Position { get; set; }

        public ArrayWriter(byte[] buffer, int position) 
        {
            Buffer = buffer;
            Position = position;
        }

        public byte GetPrevValue(int back)
        {
            return Buffer[Position - back];
        }

        public void Write(byte value)
        {
            Buffer[Position++] = value;
        }

        public void Write(byte[] array, int offset, int length)
        {
            System.Buffer.BlockCopy(array, offset, Buffer, Position, length);
            Position += length;
        }
    }
}
