using System.IO.Compression;

namespace Deflate
{
    internal class Program
    {
        private static readonly Deflator _deflateDecompressor = new Deflator();

        static void Main(string[] args)
        {
            byte[] originalData = GenerateInput();
            byte[] compressedData = Compress(originalData);
            byte[] decompressedData = Decompress(compressedData);

            bool isDecompressionValid = Validate(originalData, decompressedData);
            Console.WriteLine(isDecompressionValid);

            Console.WriteLine(compressedData.Length / (float)(1024));

            Console.ReadLine();
        }

        private static byte[] GenerateInput() 
        {
            return File.ReadAllBytes("D:\\Darbvirsma\\File4.html");
        }

        public static byte[] Compress(byte[] decompressedDaa) 
        {
            using (MemoryStream inputStream = new MemoryStream(decompressedDaa))
            using (MemoryStream outputStream = new MemoryStream())
            using (DeflateStream compressor = new DeflateStream(outputStream, CompressionLevel.SmallestSize))
            {
                inputStream.CopyTo(compressor);
                compressor.Flush();

                return outputStream.ToArray();
            }
        }
        private static byte[] Decompress(byte[] compressedData) 
        {
            BitReadOnlyStream bitStream = new BitReadOnlyStream(compressedData);
            byte[] buffer = new byte[1024 * 1024 * 128];
            ArrayWriter writer = new ArrayWriter(buffer, 0);

            int red = _deflateDecompressor.Decompress(ref bitStream, writer);
            Span<byte> output = buffer.AsSpan()[..red];

            return output.ToArray();
        }

        public static bool Validate(byte[] compressed, byte[] decompressed) 
        {
            if (compressed.Length != decompressed.Length) return false;

            for (int i = 0; i < compressed.Length; i++)
            {
                if (compressed[i] != decompressed[i]) return false;
            }

            return true;
        }
    }
}