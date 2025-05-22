namespace Deflate
{
    public interface IPrefixCodeTree
    {
        short Read(ref BitReadOnlyStream input);
    }
}
