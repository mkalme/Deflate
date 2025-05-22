namespace Deflate
{
    public interface IDeflateBlock
    {
        int Decompress(ref BitReadOnlyStream input, IWriter output);
    }
}
