namespace CodeBuilder
{
    public class Interface : IType
    {
        public Interface(string name, string ns = null, string documentation = null, AccessModifier access = AccessModifier.Public, Event[] events = null, Property[] properties = null, Method[] methods = null, IType[] interfaces = null)
        {
            this.Name = name;
            this.Namespace = ns;
            this.Access = access;
            this.Documentation = documentation;
            this.Interfaces = interfaces ?? new IType[0];
            this.Properties = properties ?? new Property[0];
            this.Methods = methods ?? new Method[0];
            this.Events = events ?? new Event[0];
        }

        public string Name { get; }
        
        public string Namespace { get; }

        public string Documentation { get; }

        public AccessModifier Access { get; }

        public Event[] Events { get; }

        public IType[] Interfaces { get; }

        public Property[] Properties { get; }

        public Method[] Methods { get; }
    }
}
