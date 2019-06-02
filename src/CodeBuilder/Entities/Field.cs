namespace CodeBuilder
{
    public class Field
    {
        public Field(string name, IType type, ScopeModifier scope = ScopeModifier.Instance, AccessModifier access = AccessModifier.Private)
        {
            this.Type = type;
            this.Name = name;
            this.Scope = scope;
            this.Access = access;
        }

        public string Name { get; }

        public IType Type { get; }

        public ScopeModifier Scope { get; }

        public AccessModifier Access { get; }
    }
}
