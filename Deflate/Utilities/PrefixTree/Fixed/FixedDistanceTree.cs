namespace Deflate
{
    public class FixedDistanceTree : IPrefixCodeTree
    {
        public static FixedDistanceTree Singleton { get; } = new FixedDistanceTree();

        public short Read(ref BitReadOnlyStream input)
        {
            return (short)input.ReadNextBitsReversed(5);
        }
    }
}
