namespace Deflate
{
    public class FixedBlock : PrefixEncodedBlock
    {
        public override int Decompress(ref BitReadOnlyStream input, IWriter output)
        {
            return ReadBlock(ref input, output, FixedHuffmanTree.Singleton, FixedDistanceTree.Singleton);
        }
    }
}
