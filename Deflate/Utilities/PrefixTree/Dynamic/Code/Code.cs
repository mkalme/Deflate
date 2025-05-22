namespace Deflate
{
    public readonly struct Code
    {
        public short StoredLiteral { get; }
        public short ActualCode { get; }
        public byte Length { get; }

        public Code(short storedLiteral, short actualCode, byte length)
        {
            StoredLiteral = storedLiteral;
            ActualCode = actualCode;
            Length = length;
        }
    }
}
