namespace Deflate
{
    public class ValueNode : INode
    {
        public short Value { get; set; }

        public short Read(ref BitReadOnlyStream input)
        {
            return Value;
        }
        public void AddValue(short value, short code, int index)
        {
            Value = value;
        }
    }
}
