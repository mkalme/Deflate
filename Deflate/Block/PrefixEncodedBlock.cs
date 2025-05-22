namespace Deflate
{
    public abstract class PrefixEncodedBlock : IDeflateBlock
    {
        protected int ReadBlock(ref BitReadOnlyStream input, IWriter output, IPrefixCodeTree literalTree, IPrefixCodeTree distanceTree)
        {
            short symbol;
            int prevOutputIndex = output.Position;

            while (!SymbolHelper.IsEndOfBlock(symbol = literalTree.Read(ref input)))
            {
                if (SymbolHelper.IsLiteral(symbol))
                {
                    output.Write((byte)symbol);
                }
                else
                {
                    LengthSymbol lengthSymbol = LengthSymbol.GetSymbol(symbol);
                    int length = lengthSymbol.GetLengthFromOffset(input.ReadNextBits(lengthSymbol.OffsetBits));

                    DistanceSymbol distanceSymbol = DistanceSymbol.Table[distanceTree.Read(ref input)];
                    int distance = distanceSymbol.GetDistanceFromOffset(input.ReadNextBits(distanceSymbol.OffsetBits));

                    SymbolHelper.ReadBackReference(length, distance, output);
                }
            }

            return output.Position - prevOutputIndex;
        }

        public abstract int Decompress(ref BitReadOnlyStream input, IWriter output);
    }
}
