namespace CodeBuilder
{
    public static class ParameterExtensions
    {
        public static Parameter WithName(this Parameter @this, string value) => new Parameter(value,
                                                                                              @this.Type,
                                                                                              documentation: @this.Documentation);

        public static Parameter WithType(this Parameter @this, IType value) => new Parameter(@this.Name,
                                                                                             value,
                                                                                             documentation: @this.Documentation);

        public static Parameter WithDocumentation(this Parameter @this, string value) => new Parameter(@this.Name,
                                                                                             @this.Type,
                                                                                             documentation:value);
    }
}
