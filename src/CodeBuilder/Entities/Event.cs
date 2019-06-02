namespace CodeBuilder
{
    public class Event
    {
        public Event(string name, string documentation = null, IType handlerType = null, ScopeModifier scope = ScopeModifier.Instance, AccessModifier access = AccessModifier.Public)
        {
            this.HandlerType = handlerType ?? new Type(Modules.System, "EventHandler");
            this.Name = name;
            this.Documentation = documentation;
            this.Scope = scope;
            this.Access = access;
        }

        public string Name { get; }

        public string Documentation { get; }

        public IType HandlerType { get; }

        public ScopeModifier Scope { get; }

        public AccessModifier Access { get; }
    }
}
