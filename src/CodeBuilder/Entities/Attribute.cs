namespace CodeBuilder
{
    public class Attribute
    {
        public Attribute(IType type, string[] arguments = null)
        {
            this.Type = type;
            this.Arguments = arguments ?? new string[0];
        }

        public IType Type { get; }

        public string[] Arguments { get; }
    }
}
