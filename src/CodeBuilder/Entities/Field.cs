namespace CodeBuilder
{
    public class Field
    {
        public Field(string name, IType type, bool isReadonly = false, ScopeModifier scope = ScopeModifier.Instance, AccessModifier access = AccessModifier.Private)
        {
            this.Type = type;
            this.Name = name;
            this.IsReadonly = isReadonly;
            this.Scope = scope;
            this.Access = access;
        }

        public string Name { get; }

        public bool IsReadonly { get; }

        public IType Type { get; }

        public ScopeModifier Scope { get; }

        public AccessModifier Access { get; }
    }
}
