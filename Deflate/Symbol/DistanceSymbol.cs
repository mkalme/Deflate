using System.Runtime.CompilerServices;

namespace Deflate
{
    public readonly struct DistanceSymbol
    {
        public int BaseDistance { get; }
        public byte OffsetBits { get; }

        public static readonly DistanceSymbol[] Table = new DistanceSymbol[] 
        {
            new DistanceSymbol(1, 0),
            new DistanceSymbol(2, 0),
            new DistanceSymbol(3, 0),
            new DistanceSymbol(4, 0),
            new DistanceSymbol(5, 1),
            new DistanceSymbol(7, 1),
            new DistanceSymbol(9, 2),
            new DistanceSymbol(13, 2),
            new DistanceSymbol(17, 3),
            new DistanceSymbol(25, 3),
            new DistanceSymbol(33, 4),
            new DistanceSymbol(49, 4),
            new DistanceSymbol(65, 5),
            new DistanceSymbol(97, 5),
            new DistanceSymbol(129, 6),
            new DistanceSymbol(193, 6),
            new DistanceSymbol(257, 7),
            new DistanceSymbol(385, 7),
            new DistanceSymbol(513, 8),
            new DistanceSymbol(769, 8),
            new DistanceSymbol(1025, 9),
            new DistanceSymbol(1537, 9),
            new DistanceSymbol(2049, 10),
            new DistanceSymbol(3073, 10),
            new DistanceSymbol(4097, 11),
            new DistanceSymbol(6145, 11),
            new DistanceSymbol(8193, 12),
            new DistanceSymbol(12289, 12),
            new DistanceSymbol(16385, 13),
            new DistanceSymbol(24577, 13),
        };

        public DistanceSymbol(int baseDistance, byte offsetBits)
        {
            BaseDistance = baseDistance;
            OffsetBits = offsetBits;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public int GetDistanceFromOffset(int offset)
        {
            return BaseDistance + offset;
        }
    }
}
