using System;
using System.Linq;

namespace CodeBuilder
{
    public static class MethodExtensions
    {
        public static Method WithName(this Method @this, string name) => new Method(name,
                                                                                     documentation: @this.Documentation,
                                                                                     returnType: @this.ReturnType,
                                                                                     @override: @this.Override,
                                                                                     access: @this.Access,
                                                                                     scope: @this.Scope,
                                                                                     async: @this.Sync,
                                                                                     implementation: @this.Implementation,
                                                                                     parameters: @this.Parameters,
                                                                                     body: @this.Body,
                                                                                     attributes: @this.Attributes);

        public static Method WithDocumentation(this Method @this, string value) => new Method(@this.Name,
                                                                                     documentation: value,
                                                                                     returnType: @this.ReturnType,
                                                                                     @override: @this.Override,
                                                                                     access: @this.Access,
                                                                                     scope: @this.Scope,
                                                                                     async: @this.Sync,
                                                                                     implementation: @this.Implementation,
                                                                                     parameters: @this.Parameters,
                                                                                     body: @this.Body,
                                                                                     attributes: @this.Attributes);

        public static Method WithScope(this Method @this, ScopeModifier value) => new Method(@this.Name,
                                                                                             documentation: @this.Documentation,
                                                                                             returnType: @this.ReturnType,
                                                                                             @override: @this.Override,
                                                                                             access: @this.Access,
                                                                                             scope: value,
                                                                                             async: @this.Sync,
                                                                                             implementation: @this.Implementation,
                                                                                             parameters: @this.Parameters,
                                                                                             body: @this.Body,
                                                                                     attributes: @this.Attributes);

        public static Method WithSync(this Method @this, SyncModifier value = SyncModifier.Asynchronous) => new Method(@this.Name,
                                                                                                             documentation: @this.Documentation,
                                                                                                             returnType: @this.ReturnType,
                                                                                                             @override: @this.Override,
                                                                                                             access: @this.Access,
                                                                                                             scope: @this.Scope,
                                                                                                             async: value,
                                                                                                             implementation: @this.Implementation,
                                                                                                             parameters: @this.Parameters,
                                                                                                             body: @this.Body,
                                                                                     attributes: @this.Attributes);


        public static Method WithAccess(this Method @this, AccessModifier value) => new Method(@this.Name,
                                                                                             documentation: @this.Documentation,
                                                                                             returnType: @this.ReturnType,
                                                                                             @override: @this.Override,
                                                                                             access: value,
                                                                                             scope: @this.Scope,
                                                                                             async: @this.Sync,
                                                                                             implementation: @this.Implementation,
                                                                                             parameters: @this.Parameters,
                                                                                             body: @this.Body,
                                                                                     attributes: @this.Attributes);


        public static Method WithOverride(this Method @this, OverrideModifier value) => new Method(@this.Name,
                                                                                             documentation: @this.Documentation,
                                                                                             returnType: @this.ReturnType,
                                                                                             @override: value,
                                                                                             access: @this.Access,
                                                                                             scope: @this.Scope,
                                                                                             async: @this.Sync,
                                                                                             implementation: @this.Implementation,
                                                                                             parameters: @this.Parameters,
                                                                                             body: @this.Body,
                                                                                     attributes: @this.Attributes);


        public static Method WithImplementation(this Method @this, ImplementationModifier value) => new Method(@this.Name,
                                                                                                                 documentation: @this.Documentation,
                                                                                                                 returnType: @this.ReturnType,
                                                                                                                 @override: @this.Override,
                                                                                                                 access: @this.Access,
                                                                                                                 scope: @this.Scope,
                                                                                                                 async: @this.Sync,
                                                                                                                 implementation: value,
                                                                                                                 parameters: @this.Parameters,
                                                                                                               body: @this.Body,
                                                                                     attributes: @this.Attributes);


        public static Method WithParameter(this Method @this, Parameter value) => new Method(@this.Name,
                                                                                             documentation: @this.Documentation,
                                                                                             returnType: @this.ReturnType,
                                                                                             @override: @this.Override,
                                                                                             access: @this.Access,
                                                                                             scope: @this.Scope,
                                                                                             async: @this.Sync,
                                                                                             implementation: @this.Implementation,
                                                                                             parameters: @this.Parameters.Concat(new[] { value }).ToArray(),
                                                                                             body: @this.Body,
                                                                                     attributes: @this.Attributes);

        public static Method WithBody(this Method @this, IInstruction value) => new Method(@this.Name,
                                                                                     documentation: @this.Documentation,
                                                                                     returnType: @this.ReturnType,
                                                                                     @override: @this.Override,
                                                                                     access: @this.Access,
                                                                                     scope: @this.Scope,
                                                                                     async: @this.Sync,
                                                                                     implementation: @this.Implementation,
                                                                                     parameters: @this.Parameters,
                                                                                     body: new Body(value),
                                                                                     attributes: @this.Attributes);

        public static Method WithAttribute(this Method @this, IType type, params string[] arguments) => new Method(@this.Name,
                                                                                     documentation: @this.Documentation,
                                                                                     returnType: @this.ReturnType,
                                                                                     @override: @this.Override,
                                                                                     access: @this.Access,
                                                                                     scope: @this.Scope,
                                                                                     async: @this.Sync,
                                                                                     implementation: @this.Implementation,
                                                                                     parameters: @this.Parameters,
                                                                                     body: @this.Body,
                                                                                     attributes: @this.Attributes.Concat(new[] { new Attribute(type, arguments) }).ToArray());
                                                                                     

        public static Method WithParameter(this Method @this, string name, IType type, Func<Parameter, Parameter> initializer = null)
        {
            var parameter = new Parameter(name, type);
            if (initializer != null)
            {
                parameter = initializer(parameter);
            }
            return @this.WithParameter(parameter);
        }

        public static Method WithParameter<T>(this Method @this, string name, Func<Parameter, Parameter> initializer = null)
        {
            return @this.WithParameter(name, typeof(T).ToBuilder(), initializer);
        }
    }
}
