namespace Deflate
{
    public class HuffmanTree : IPrefixCodeTree
    {
        private BranchNode RootNode { get; }

        public HuffmanTree(BranchNode rootNode) 
        {
            RootNode = rootNode;
        }

        public short Read(ref BitReadOnlyStream input) 
        {
            return RootNode.Read(ref input);
        }

        public static HuffmanTree FromLiteralLengths(Span<LiteralLength> lengths)
        {
            CodeLength[] c = new CodeLength[16];
            for (int i = 0; i < c.Length; i++)
            {
                c[i] = new CodeLength((short)i, new List<short>());
            }

            for (int i = 0; i < lengths.Length; i++)
            {
                LiteralLength l = lengths[i];
                c[l.Length].Literals.Add(l.StoredLiteral);
            }

            int ll = 0;
            for (int i = 0; i < c.Length; i++)
            {
                c[i].Literals = c[i].Literals.Order().ToList();

                if (c[i].Length > 0 && c[i].Literals.Count > 0) ll += c[i].Literals.Count;
            }

            Span<Code> huffmanTree = stackalloc Code[ll];
            FillCode(c, huffmanTree);

            BranchNode rootNode = new BranchNode();
            for (int i = 0; i < huffmanTree.Length; i++)
            {
                Code code = huffmanTree[i];
                rootNode.AddValue(code.StoredLiteral, code.ActualCode, code.Length - 1);
            }

            return new HuffmanTree(rootNode);
        }

        private static void FillCode(ReadOnlySpan<CodeLength> codeLengths, Span<Code> output)
        {
            short[] next_code = new short[16];

            int code = 0;
            for (int bits = 1; bits <= 15; bits++)
            {
                code = (code + codeLengths[bits - 1].Literals.Count) << 1;
                next_code[bits] = (short)code;
            }

            int index = 0;
            for (int i = 0; i < codeLengths.Length; i++) 
            {
                CodeLength l = codeLengths[i];
                if (l.Length == 0 || l.Literals.Count == 0) continue;

                for (int j = 0; j < l.Literals.Count; j++) 
                {
                    output[index++] = new Code(l.Literals[j], next_code[i], (byte)i);
                    next_code[i]++;
                }
            }
        }
    }
}
