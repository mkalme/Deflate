using System.Runtime.CompilerServices;

namespace Deflate
{
    public struct BitReadOnlyStream
    {
        public byte[] Buffer { get; }
        public int Position { get; set; }
        public int PositionInByte { get; set; }

        public BitReadOnlyStream(byte[] source, int position = 0, int positionInByte = 0)
        {
            Buffer = source;
            Position = position;
            PositionInByte = positionInByte;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public void AdvanceToNearestFullByte()
        {
            if (PositionInByte == 0) return;

            Position++;
            PositionInByte = 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public void IncrementPositionInsideByte(int value)
        {
            PositionInByte += value;
            if (PositionInByte >= 8)
            {
                Position += PositionInByte >> 3;
                PositionInByte &= 7;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public byte ReadCurrentFullByte()
        {
            return Buffer[Position++];
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public int ReadNextBits(int length)
        {
            int output = 0, offset = 0;

            while (length > 0)
            {
                int read = Math.Min(8 - PositionInByte, length);

                output |= ReadNBits(Buffer[Position], PositionInByte, read) << offset;
                offset += read;
                length -= read;

                IncrementPositionInsideByte(read);
            }

            return output;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public int ReadNextBitsReversed(int length) 
        {
            return ReverseBits(ReadNextBits(length), length);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public bool ReadNextBit()
        {
            int bit = ReadNBits(Buffer[Position], PositionInByte, 1);
            IncrementPositionInsideByte(1);

            return bit == 1;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public ushort ReadUInt16()
        {
            int b1 = ReadNextBits(8), b2 = ReadNextBits(8);
            return (ushort)(b2 << 8 | b1);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public int ReverseBits(int input, int kBits) 
        {
            int output = 0;
            for (int i = 0; i < kBits; i++)
            {
                output |= (input & (1 << i)) != 0 ? 1 << (kBits - 1 - i) : 0;
            }

            return output;
        }

        public bool IsEnd() 
        {
            if (Position > Buffer.Length - 1) return true;
            return Position == Buffer.Length - 1 && PositionInByte == 8;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        private static int ReadNBits(byte value, int index, int count)
        {
            int moveBy = 8 - count - index;
            return ((byte)(value << moveBy)) >> (8 - count);
        }
    }
}
