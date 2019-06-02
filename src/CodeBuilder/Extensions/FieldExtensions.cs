namespace CodeBuilder
{
    public static class FieldExtensions
    {
        public static Field WithScope(this Field @this, ScopeModifier value) => new Field(@this.Name,
                                                                                          @this.Type,
                                                                                           scope: value,
                                                                                           access: @this.Access);

        public static Field WithAccess(this Field @this, AccessModifier value) => new Field(@this.Name,
                                                                                            @this.Type,
                                                                                             scope: @this.Scope,
                                                                                            access: value);

        public static Field WithType(this Field @this, IType value) => new Field(@this.Name,
                                                                                  value,
                                                                                  scope: @this.Scope,
                                                                                  access: @this.Access);
    }
}
