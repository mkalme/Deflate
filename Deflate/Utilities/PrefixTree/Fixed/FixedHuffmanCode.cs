using System.Runtime.CompilerServices;

namespace Deflate
{
    public struct FixedHuffmanCode
    {
        public byte Bits { get; set; }
        public short CodeStart { get; set; }
        public ReadOnlyMemory<short> Symbols { get; set; }

        public static readonly FixedHuffmanCode[] Codes = new FixedHuffmanCode[] {
            new FixedHuffmanCode(8, 0b_00110000, SymbolHelper.CreateRange(0, 143)),
            new FixedHuffmanCode(9, 0b_110010000, SymbolHelper.CreateRange(144, 255)),
            new FixedHuffmanCode(7, 0b_0000000, SymbolHelper.CreateRange(256, 279)),
            new FixedHuffmanCode(8, 0b_11000000, SymbolHelper.CreateRange(280, 287))
        };

        public FixedHuffmanCode(byte bits, short start, ReadOnlyMemory<short> symbols) 
        {
            Bits = bits;
            CodeStart = start;
            Symbols = symbols;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public bool IsSymbolWithinRange(int symbol) 
        {
            return symbol >= CodeStart && (symbol - CodeStart) < Symbols.Length;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public short ConvertToValidSymbol(int invalidSymbol) 
        {
            return Symbols.Span[invalidSymbol - CodeStart];
        }
    }
}
