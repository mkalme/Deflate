namespace Deflate
{
    public readonly struct LiteralLength
    {
        public short StoredLiteral { get; }
        public short Length { get; }

        public LiteralLength(short storedLiteral, short length)
        {
            StoredLiteral = storedLiteral;
            Length = length;
        }
    }
}
