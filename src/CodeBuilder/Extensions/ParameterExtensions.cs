using System.Linq;

namespace CodeBuilder
{
    public static class ParameterExtensions
    {
        public static Parameter WithName(this Parameter @this, string value) => new Parameter(value,
                                                                                              @this.Type,
                                                                                              documentation: @this.Documentation,
                                                                                              attributes:  @this.Attributes);

        public static Parameter WithType(this Parameter @this, IType value) => new Parameter(@this.Name,
                                                                                             value,
                                                                                             documentation: @this.Documentation,
                                                                                              attributes:  @this.Attributes);

        public static Parameter WithDocumentation(this Parameter @this, string value) => new Parameter(@this.Name,
                                                                                             @this.Type,
                                                                                             documentation:value,
                                                                                              attributes:  @this.Attributes);

        public static Parameter WithAttribute(this Parameter @this, IType type, params string[] arguments) => new Parameter(@this.Name,
                                                                                             @this.Type,
                                                                                             documentation:@this.Documentation,
                                                                                              attributes: @this.Attributes.Concat(new[] { new Attribute(type, arguments) }).ToArray());
    }
}
