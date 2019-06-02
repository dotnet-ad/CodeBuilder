namespace CodeBuilder
{
    public static class PropertyExtensions
    {
        public static Property WithDocumentation(this Property @this, string value) => new Property(@this.Name,
                                                                                                     @this.Type,
                                                                                                    documentation: value,
                                                                                                    getter: @this.Getter,
                                                                                                    setter: @this.Setter,
                                                                                                    scope: @this.Scope,
                                                                                                    access: @this.Access);
        
        public static Property WithScope(this Property @this, ScopeModifier value) => new Property(@this.Name,
                                                                                                   @this.Type,
                                                                                                   documentation: @this.Documentation,
                                                                                                   getter: @this.Getter,
                                                                                                   setter: @this.Setter,
                                                                                                   scope: value,
                                                                                                   access: @this.Access);

        public static Property WithAccess(this Property @this, AccessModifier value) => new Property(@this.Name,
                                                                                                     @this.Type,
                                                                                             documentation: @this.Documentation,
                                                                                             getter: @this.Getter,
                                                                                             setter: @this.Setter,
                                                                                             scope: @this.Scope,
                                                                                             access: value);

        public static Property WithType(this Property @this, IType value) => new Property(@this.Name,
                                                                                  value,
                                                                                  documentation: @this.Documentation,
                                                                                  getter: @this.Getter,
                                                                                  setter: @this.Setter,
                                                                                  scope: @this.Scope,
                                                                                          access: @this.Access);

        public static Property WithGetter(this Property @this, Body getter) => new Property(@this.Name,
                                                                                              @this.Type,
                                                                                              documentation: @this.Documentation,
                                                                                              getter: getter,
                                                                                              setter: @this.Setter,
                                                                                              scope: @this.Scope,
                                                                                              access: @this.Access);

        public static Property WithSetter(this Property @this, Body setter) => new Property(@this.Name,
                                                                                              @this.Type,
                                                                                              documentation: @this.Documentation,
                                                                                              getter: @this.Getter,
                                                                                              setter: setter,
                                                                                              scope: @this.Scope,
                                                                                              access: @this.Access);

        public static Property WithAutoGetter(this Property @this) => @this.WithGetter(Body.Auto);

        public static Property WithAutoSetter(this Property @this) => @this.WithSetter(Body.Auto);

        public static Property WithGetter(this Property @this, IInstruction instruction) => @this.WithGetter(new Body(instruction));

        public static Property WithSetter(this Property @this, IInstruction instruction) => @this.WithSetter(new Body(instruction));

        public static Property WithFieldGetter(this Property @this, string fieldName) => @this.WithGetter(new Statement($"this.{fieldName};"));

        public static Property WithFieldSetter(this Property @this, string fieldName) => @this.WithSetter(new Statement($"this.{fieldName} = value;"));
    }
}
