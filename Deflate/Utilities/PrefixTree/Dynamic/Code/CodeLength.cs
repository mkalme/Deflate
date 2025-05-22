namespace Deflate
{
    public struct CodeLength
    {
        public short Length { get; set; }
        public IList<short> Literals { get; set; }

        public CodeLength(short length, IList<short> literals)
        {
            Length = length;
            Literals = literals;
        }
    }
}
