namespace Deflate
{
    public class Deflator
    {
        private IDeflateBlock StoreBlock { get; set; } = new StoreBlock();
        private IDeflateBlock FixedBlock { get; set; } = new FixedBlock();
        private IDeflateBlock DynamicBlock { get; set; } = new DynamicBlock();

        public int Decompress(ref BitReadOnlyStream input, IWriter output)
        {
            int red = 0;

            while (!input.IsEnd())
            {
                bool isFinal = IsFinal(ref input);

                int prev = red;

                BlockType blockType = (BlockType)input.ReadNextBits(2);                
                switch (blockType)
                {
                    case BlockType.Store:
                        red += StoreBlock.Decompress(ref input, output);
                        break;
                    case BlockType.FixedCode:
                        red += FixedBlock.Decompress(ref input, output);
                        break;
                    case BlockType.DynamicCode:
                        red += DynamicBlock.Decompress(ref input, output);
                        break;
                }

                Console.WriteLine((red - prev) + " | " + blockType);

                if (isFinal) break;
            }

            return red;
        }

        private static bool IsFinal(ref BitReadOnlyStream input) 
        {
            return input.ReadNextBit();
        }
    }
}
