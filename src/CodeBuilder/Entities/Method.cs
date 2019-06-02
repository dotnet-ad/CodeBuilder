namespace CodeBuilder
{
    public class Method
    {
        public Method(string name, string documentation = null, ScopeModifier scope = ScopeModifier.Instance, OverrideModifier @override = OverrideModifier.None, AccessModifier access = AccessModifier.Public, ImplementationModifier implementation = ImplementationModifier.SingleFile, SyncModifier async = SyncModifier.Synchronous, IType returnType = null, Parameter[] parameters = null, Body body = null,  Attribute[] attributes = null)
        {
            this.Name = name;
            this.Documentation = documentation;
            this.Parameters = parameters ?? new Parameter[0];
            this.Attributes = attributes ?? new Attribute[0];
            this.Body = body;
            this.ReturnType = returnType;
            this.Sync = async;
            this.Override = @override;
            this.Scope = scope;
            this.Access = access;
            this.Implementation = implementation;
        }

        public Attribute[] Attributes { get; }

        public IType ReturnType { get; }

        public ScopeModifier Scope { get; }

        public AccessModifier Access { get; }

        public SyncModifier Sync { get; }

        public OverrideModifier Override { get; }

        public ImplementationModifier Implementation { get; }

        public string Name { get; }

        public string Documentation { get; }

        public Parameter[] Parameters { get; }

        public Body Body { get; }
    }
}
