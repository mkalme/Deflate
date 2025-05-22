namespace Deflate
{
    public class FixedHuffmanTree : IPrefixCodeTree
    {
        public static FixedHuffmanTree Singleton { get; } = new FixedHuffmanTree();

        public short Read(ref BitReadOnlyStream input)
        {
            int startByte = input.Position, startBit = input.PositionInByte;

            for (int i = 0; i < FixedHuffmanCode.Codes.Length; i++)
            {
                FixedHuffmanCode code = FixedHuffmanCode.Codes[i];

                int symbol = input.ReadNextBitsReversed(code.Bits);
                if (!code.IsSymbolWithinRange(symbol))
                {
                    input.Position = startByte;
                    input.PositionInByte = startBit;

                    continue;
                }

                return code.ConvertToValidSymbol(symbol);
            }

            return short.MinValue;
        }
    }
}
