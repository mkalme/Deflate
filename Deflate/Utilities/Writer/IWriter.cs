namespace Deflate
{
    public interface IWriter
    {
        int Position { get; }

        byte GetPrevValue(int back);
        void Write(byte value);
        void Write(byte[] array, int offset, int length);
    }
}
