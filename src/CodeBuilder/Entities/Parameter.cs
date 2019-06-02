namespace CodeBuilder
{
    public class Parameter
    {
        public Parameter(string name, IType type, string documentation = null)
        {
            this.Type = type;
            this.Name = name;
            this.Documentation = documentation;
        }

        public string Name { get; }

        public string Documentation { get; }

        public IType Type { get; }
    }
}
