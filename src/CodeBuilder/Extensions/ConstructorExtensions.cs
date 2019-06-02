using System;
using System.Linq;

namespace CodeBuilder
{
    public static class ConstructorExtensions
    {
        public static Constructor WithDocumentation(this Constructor @this, string value) => new Constructor(
                                                                                     documentation: value,
                                                                                     access: @this.Access,
                                                                                     parameters: @this.Parameters,
                                                                                     body: @this.Body);

        public static Constructor WithAccess(this Constructor @this, AccessModifier value) => new Constructor(
                                                                                             documentation: @this.Documentation,
                                                                                             access: value,
                                                                                             parameters: @this.Parameters,
                                                                                             body: @this.Body);

        public static Constructor WithParameter(this Constructor @this, Parameter value) => new Constructor(
                                                                                             documentation: @this.Documentation,
                                                                                             access: @this.Access,
                                                                                             parameters: @this.Parameters.Concat(new[] { value }).ToArray(),
                                                                                             body: @this.Body);

        public static Constructor WithBody(this Constructor @this, IInstruction value) => new Constructor(
                                                                                     documentation: @this.Documentation,
                                                                                     access: @this.Access,
                                                                                     parameters: @this.Parameters,
                                                                                     body: new Body(value));


        public static Constructor WithParameter(this Constructor @this, string name, IType type, Func<Parameter, Parameter> initializer = null)
        {
            var parameter = new Parameter(name, type);
            if (initializer != null)
            {
                parameter = initializer(parameter);
            }
            return @this.WithParameter(parameter);
        }

        public static Constructor WithParameter<T>(this Constructor @this, string name, Func<Parameter, Parameter> initializer = null)
        {
            return @this.WithParameter(name, typeof(T).ToBuilder(), initializer);
        }
    }
}
