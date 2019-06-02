using System;
using System.Linq;

namespace CodeBuilder
{
    public static class ModuleExtensions
    {
        public static Module WithTypes(this Module @this, params IType[] types) => new Module(@this.Name, @this.Types.Concat(types).ToArray(), @this.Imports);

        public static Module WithClass(this Module @this, string name, Func<Class, Class> initializer = null) 
        {
            var newClass = new Class(@this.Name, name);
            initializer?.Invoke(newClass);
            return new Module(@this.Name, @this.Types.Concat(new [] { newClass }).ToArray(), @this.Imports);
        }

        public static Module WithImport(this Module @this, Module module)
        {
            return new Module(@this.Name, @this.Types, @this.Imports.Concat(new[] { module }).ToArray());
        }

        public static Module WithImport(this Module @this, string module)
        {
            return new Module(@this.Name, @this.Types, @this.Imports.Concat(new[] { new Module(module) }).ToArray());
        }

        public static Module WithImport<T>(this Module @this)
        {
            return new Module(@this.Name, @this.Types, @this.Imports.Concat(new[] { new Module(typeof(T).Namespace) }).ToArray());
        }
    }
}
