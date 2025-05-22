namespace Deflate
{
    public class DynamicBlock : PrefixEncodedBlock
    {
        private static readonly int[] _codeLengthsForAlphabet = new int[] {
            16, 17, 18, 0, 8, 7, 9, 6, 10, 5, 11, 4, 12, 3, 13, 2, 14, 1, 15
        };

        public override int Decompress(ref BitReadOnlyStream input, IWriter output)
        {
            int hlit = input.ReadNextBits(5), hdist = input.ReadNextBits(5), hclen = input.ReadNextBits(4);

            HuffmanTree codeLengthTree = CreateCodeLengthHuffmanTree(ref input, hclen);
            HuffmanTree literalLengthTree = CreateHuffmanTreeFromLengthCodes(ref input, codeLengthTree, hlit + 257);
            HuffmanTree distanceLengthTree = CreateHuffmanTreeFromLengthCodes(ref input, codeLengthTree, hdist + 1);

            return ReadBlock(ref input, output, literalLengthTree, distanceLengthTree);
        }

        private HuffmanTree CreateCodeLengthHuffmanTree(ref BitReadOnlyStream input, int hclen) 
        {
            LiteralLength[] codeLengths = new LiteralLength[hclen + 4];
            for (int i = 0; i < hclen + 4; i++)
            {
                codeLengths[i] = new LiteralLength((short)_codeLengthsForAlphabet[i], (short)input.ReadNextBits(3));
            }

            return HuffmanTree.FromLiteralLengths(codeLengths);
        }
        private static HuffmanTree CreateHuffmanTreeFromLengthCodes(ref BitReadOnlyStream input, HuffmanTree codeLengthHuffmanTree, int n) 
        {
            byte[] lengthsOfLiterals = new byte[n];

            int index = 0;
            while (index < lengthsOfLiterals.Length)
            {
                short a = codeLengthHuffmanTree.Read(ref input);

                if (a < 16)
                {
                    lengthsOfLiterals[index] = (byte)a;
                    index++;
                }
                else if (a == 16)
                {
                    int repeat = input.ReadNextBits(2) + 3;

                    for (int i = 0; i < repeat; i++)
                    {
                        lengthsOfLiterals[index + i] = lengthsOfLiterals[index - 1];
                    }

                    index += repeat;
                }
                else if (a == 17)
                {
                    index += input.ReadNextBits(3) + 3;
                }
                else if (a == 18)
                {
                    index += input.ReadNextBits(7) + 11;
                }
            }

            LiteralLength[] lc = new LiteralLength[lengthsOfLiterals.Length];
            for (int i = 0; i < lengthsOfLiterals.Length; i++)
            {
                lc[i] = new LiteralLength((short)i, lengthsOfLiterals[i]);
            }

            return HuffmanTree.FromLiteralLengths(lc);
        }
    }
}