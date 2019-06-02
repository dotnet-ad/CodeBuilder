namespace CodeBuilder
{
    public class Parameter
    {
        public Parameter(string name, IType type, string documentation = null, Attribute[] attributes = null)
        {
            this.Type = type;
            this.Name = name;
            this.Documentation = documentation;
            this.Attributes = attributes ?? new Attribute[0];
        }

        public Attribute[] Attributes { get; }

        public string Name { get; }

        public string Documentation { get; }

        public IType Type { get; }
    }
}
