namespace CodeBuilder
{
    public class Property
    {
        public Property(string name, IType type, ScopeModifier scope = ScopeModifier.Instance, AccessModifier access = AccessModifier.Public, string documentation = null, Body getter = null, Body setter = null)
        {
            this.Type = type;
            this.Name = name;
            this.Scope = scope;
            this.Access = access;
            this.Documentation = documentation;
            this.Getter = getter;
            this.Setter = setter;
        }

        public string Name { get; }

        public ScopeModifier Scope { get; }

        public AccessModifier Access { get; }

        public string Documentation { get; }

        public IType Type { get; }

        public Body Getter { get; }

        public Body Setter { get; }
    }
}
