namespace Deflate
{
    public interface INode
    {
        short Read(ref BitReadOnlyStream input);
        void AddValue(short value, short code, int index);
    }
}
