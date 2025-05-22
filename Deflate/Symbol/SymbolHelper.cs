using System.Runtime.CompilerServices;

namespace Deflate
{
    public static class SymbolHelper
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static bool IsLiteral(short symbol) 
        {
            return symbol < 256;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static bool IsEndOfBlock(short symbol) 
        {
            return symbol == 256;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static void ReadBackReference(int length, int distance, IWriter output) 
        {
            while (length-- > 0)
            {
                output.Write(output.GetPrevValue(distance));
            }
        }

        public static short[] CreateRange(int from, int toInclusive) 
        {
            short[] output = new short[toInclusive - from + 1];

            for (int i = 0; i < output.Length; i++)
            {
                output[i] = (short)(from + i);
            }

            return output;
        }
    }
}
