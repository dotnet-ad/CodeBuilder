namespace CodeBuilder
{
    public static class EventExtensions
    {
        public static Event WithDocumentation(this Event @this, string value) => new Event(@this.Name,
                                                                                           handlerType: @this.HandlerType,
                                                                                           documentation: value,
                                                                                           scope: @this.Scope,
                                                                                           access: @this.Access);
        
        public static Event WithScope(this Event @this, ScopeModifier value) => new Event(@this.Name,
                                                                                          handlerType: @this.HandlerType,
                                                                                          documentation: @this.Documentation,
                                                                                           scope: value,
                                                                                           access: @this.Access);

        public static Event WithAccess(this Event @this, AccessModifier value) => new Event(@this.Name,
                                                                                            handlerType: @this.HandlerType,
                                                                                            documentation: @this.Documentation,
                                                                                             scope: @this.Scope,
                                                                                            access: value);

        public static Event WithHandlerType(this Event @this, IType value) => new Event(@this.Name,
                                                                                        handlerType: value,
                                                                                        documentation: @this.Documentation,
                                                                                        scope: @this.Scope,
                                                                                        access: @this.Access);
    }
}
