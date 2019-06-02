namespace CodeBuilder
{
    public class Class : Interface
    {
        public Class(string name, string documentation = null, ScopeModifier scope = ScopeModifier.Instance, AccessModifier access = AccessModifier.Public, ImplementationModifier implementation = ImplementationModifier.SingleFile, IType parent = null, Constructor[] constructors = null, Field[] fields = null, Event[] events = null, Property[] properties = null, IType[] innerTypes = null, Method[] methods = null, IType[] interfaces = null) : base(name, documentation, access, events, properties, methods, interfaces)
        {
            this.Parent = parent;
            this.Fields = fields ?? new Field[0];
            this.Scope = scope;
            this.Implementation = implementation;
            this.Constructors = constructors ?? new Constructor[0];
            this.InnerTypes = innerTypes ?? new Class[0];
        }

        public IType Parent { get; }

        public Constructor[] Constructors { get; }

        public Field[] Fields { get; }

        public IType[] InnerTypes { get; }

        public ScopeModifier Scope { get; }

        public ImplementationModifier Implementation { get; }
    }
}
