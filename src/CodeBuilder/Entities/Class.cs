﻿namespace CodeBuilder
{
    public class Class : Interface
    {
        public Class(string name, string documentation = null, ScopeModifier scope = ScopeModifier.Instance, AccessModifier access = AccessModifier.Public, ImplementationModifier implementation = ImplementationModifier.SingleFile, IType parent = null, Field[] fields = null, Event[] events = null,Property[] properties = null, Method[] methods = null, IType[] interfaces = null) : base(name, documentation, access, events, properties, methods, interfaces)
        {
            this.Parent = parent;
            this.Fields = fields ?? new Field[0];
            this.Scope = scope;
            this.Implementation = implementation;
        }

        public IType Parent { get; }

        public Field[] Fields { get; }

        public ScopeModifier Scope { get; }

        public ImplementationModifier Implementation { get; }
    }
}