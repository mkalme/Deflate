using System.Runtime.CompilerServices;

namespace Deflate
{
    public readonly struct LengthSymbol
    {
        public int BaseLength { get; }
        public byte OffsetBits { get; }

        public static readonly short BaseSymbol = 257;

        public static readonly IReadOnlyList<LengthSymbol> Table = new LengthSymbol[]
        {
            new LengthSymbol(3, 0),
            new LengthSymbol(4, 0),
            new LengthSymbol(5, 0),
            new LengthSymbol(6, 0),
            new LengthSymbol(7, 0),
            new LengthSymbol(8, 0),
            new LengthSymbol(9, 0),
            new LengthSymbol(10, 0),
            new LengthSymbol(11, 1),
            new LengthSymbol(13, 1),
            new LengthSymbol(15, 1),
            new LengthSymbol(17, 1),
            new LengthSymbol(19, 2),
            new LengthSymbol(23, 2),
            new LengthSymbol(27, 2),
            new LengthSymbol(31, 2),
            new LengthSymbol(35, 3),
            new LengthSymbol(43, 3),
            new LengthSymbol(51, 3),
            new LengthSymbol(59, 3),
            new LengthSymbol(67, 4),
            new LengthSymbol(83, 4),
            new LengthSymbol(99, 4),
            new LengthSymbol(115, 4),
            new LengthSymbol(131, 5),
            new LengthSymbol(163, 5),
            new LengthSymbol(195, 5),
            new LengthSymbol(227, 5),
            new LengthSymbol(258, 0),
        };

        public LengthSymbol(int baseLength, byte offsetBits)
        {
            BaseLength = baseLength;
            OffsetBits = offsetBits;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public int GetLengthFromOffset(int offset) 
        {
            return BaseLength + offset;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static LengthSymbol GetSymbol(short symbol)
        {
            return Table[symbol - BaseSymbol];
        }
    }
}
