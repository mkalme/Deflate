namespace Deflate
{
    public class BranchNode : INode
    {
        public INode? Zero { get; set; }
        public INode? One { get; set; }

        public short Read(ref BitReadOnlyStream input)
        {
            bool bit = input.ReadNextBit();

            if (!bit) return Zero?.Read(ref input) ?? short.MaxValue;
            return One?.Read(ref input) ?? short.MaxValue;
        }
        public void AddValue(short value, short code, int index)
        {
            int bits = ReadNBits(code, index, 1);

            INode node;
            if (index == 0)
            {
                node = new ValueNode();

                if (bits == 0) Zero = node;
                else One = node;
            }
            else 
            {
                if (bits == 0)
                {
                    Zero ??= new BranchNode();
                    node = Zero;
                }
                else 
                {
                    One ??= new BranchNode();
                    node = One;
                }
            }

            node.AddValue(value, code, --index);
        }

        private static int ReadNBits(short value, int index, int count)
        {
            int moveBy = 16 - count - index;
            return ((short)(value << moveBy)) >> (16 - count);
        }
    }
}
