using System;
using System.Linq;

namespace CodeBuilder
{
    // TODO create a generator for creating this as soon as the generator is ready
    public static class ClassExtensions
    {

        public static Class WithName(this Class @this, string value) => new Class(value,
                                                                                   documentation: @this.Documentation,
                                                                                   scope: @this.Scope,
                                                                                   access: @this.Access,
                                                                                   implementation: @this.Implementation,
                                                                                   parent: @this.Parent,
                                                                                   fields: @this.Fields,
                                                                                   events: @this.Events,
                                                                                   properties: @this.Properties,
                                                                                   methods: @this.Methods,
                                                                                   interfaces: @this.Interfaces);
        
        public static Class WithScope(this Class @this, ScopeModifier value) => new Class(@this.Name,
                                                                                   documentation: @this.Documentation,
                                                                                   scope: value,
                                                                                   access: @this.Access,
                                                                                   implementation: @this.Implementation,
                                                                                   parent: @this.Parent,
                                                                                   fields: @this.Fields,
                                                                                   events: @this.Events,
                                                                                   properties: @this.Properties,
                                                                                   methods: @this.Methods,
                                                                                   interfaces: @this.Interfaces);

        public static Class WithAccess(this Class @this, AccessModifier value) => new Class(@this.Name,
                                                                                   documentation: @this.Documentation,
                                                                                   scope: @this.Scope,
                                                                                   access: value,
                                                                                   implementation: @this.Implementation,
                                                                                   parent: @this.Parent,
                                                                                   fields: @this.Fields,
                                                                                   events: @this.Events,
                                                                                   properties: @this.Properties,
                                                                                   methods: @this.Methods,
                                                                                            interfaces: @this.Interfaces);

        public static Class WithImplementation(this Class @this, ImplementationModifier value) => new Class(@this.Name,
                                                                                   documentation: @this.Documentation,
                                                                                   scope: @this.Scope,
                                                                                   access: @this.Access,
                                                                                   implementation: value,
                                                                                   parent: @this.Parent,
                                                                                   fields: @this.Fields,
                                                                                   events: @this.Events,
                                                                                   properties: @this.Properties,
                                                                                   methods: @this.Methods,
                                                                                                            interfaces: @this.Interfaces);

        public static Class WithParent(this Class @this, IType value) => new Class(@this.Name,
                                                                                   documentation: @this.Documentation,
                                                                                   scope: @this.Scope,
                                                                                   access: @this.Access,
                                                                                   implementation: @this.Implementation,
                                                                                   parent: value,
                                                                                   fields: @this.Fields,
                                                                                   events: @this.Events,
                                                                                   properties: @this.Properties,
                                                                                   methods: @this.Methods,
                                                                                   interfaces: @this.Interfaces);


        public static Class WithProperty(this Class @this, Property value) => new Class(@this.Name,
                                                                                           documentation: @this.Documentation,
                                                                                           scope: @this.Scope,
                                                                                           access: @this.Access,
                                                                                           implementation: @this.Implementation,
                                                                                           parent: @this.Parent,
                                                                                           fields: @this.Fields,
                                                                                           events: @this.Events,
                                                                                           properties: @this.Properties.Concat(new[] { value }).ToArray(),
                                                                                           methods: @this.Methods,
                                                                                           interfaces: @this.Interfaces);


        public static Class WithField(this Class @this, Field value) => new Class(@this.Name,
                                                                                           documentation: @this.Documentation,
                                                                                           scope: @this.Scope,
                                                                                           access: @this.Access,
                                                                                           implementation: @this.Implementation,
                                                                                           parent: @this.Parent,
                                                                                           fields: @this.Fields.Concat(new[] { value }).ToArray(),
                                                                                           events: @this.Events,
                                                                                           properties: @this.Properties,
                                                                                           methods: @this.Methods,
                                                                                           interfaces: @this.Interfaces);

        public static Class WithEvent(this Class @this, Event value) => new Class(@this.Name,
                                                                                           documentation: @this.Documentation,
                                                                                           scope: @this.Scope,
                                                                                           access: @this.Access,
                                                                                           implementation: @this.Implementation,
                                                                                           parent: @this.Parent,
                                                                                           fields: @this.Fields,
                                                                                           events: @this.Events.Concat(new[] { value }).ToArray(),
                                                                                           properties: @this.Properties,
                                                                                           methods: @this.Methods,
                                                                                           interfaces: @this.Interfaces);


        public static Class WithMethod(this Class @this, Method value) => new Class(@this.Name,
                                                                                           documentation: @this.Documentation,
                                                                                           scope: @this.Scope,
                                                                                           access: @this.Access,
                                                                                           implementation: @this.Implementation,
                                                                                           parent: @this.Parent,
                                                                                           fields: @this.Fields,
                                                                                           events: @this.Events,
                                                                                           properties: @this.Properties,
                                                                                           methods: @this.Methods.Concat(new[] { value }).ToArray(),
                                                                                           interfaces: @this.Interfaces);


        public static Class WithInterface(this Class @this, IType value) => new Class(@this.Name,
                                                                                           documentation: @this.Documentation,
                                                                                           scope: @this.Scope,
                                                                                           access: @this.Access,
                                                                                           implementation: @this.Implementation,
                                                                                           parent: @this.Parent,
                                                                                           fields: @this.Fields,
                                                                                           events: @this.Events,
                                                                                           properties: @this.Properties,
                                                                                           methods: @this.Methods,
                                                                                           interfaces: @this.Interfaces.Concat(new[] { value }).ToArray());

        public static Class WithProperty(this Class @this, string name, IType type, Func<Property, Property> initializer = null)
        {
            var property = new Property(name, type);
            if (initializer != null) 
            {
                property = initializer(property);
            } 
            return @this.WithProperty(property);
        }

        public static Class WithField(this Class @this, string name, IType type, Func<Field, Field> initializer = null)
        {
            var field = new Field(name, type);
            if (initializer != null)
            {
                field = initializer(field);
            }
            return @this.WithField(field);
        }

        public static Class WithEvent(this Class @this, string name, IType type = null, Func<Event, Event> initializer = null)
        {
            var field = new Event(name, handlerType: type);
            if (initializer != null)
            {
                field = initializer(field);
            }
            return @this.WithEvent(field);
        }

        public static Class WithMethod(this Class @this, string name, IType returnType, Func<Method, Method> initializer = null)
        {
            var method = new Method(name, returnType: returnType);
            if (initializer != null)
            {
                method = initializer(method);
            }
            return @this.WithMethod(method);
        }

        public static Class WithAsyncMethod(this Class @this, string name, IType returnType, Func<Method, Method> initializer = null)
        {
            var method = new Method(name, returnType: returnType, async: SyncModifier.Asynchronous);
            if (initializer != null)
            {
                method = initializer(method);
            }
            return @this.WithMethod(method);
        }

        public static Class WithMethod<T>(this Class @this, string name, Func<Method, Method> initializer = null)
        {
            return @this.WithMethod(name, typeof(T).ToBuilder(), initializer);
        }

        public static Class WithAsyncMethod<T>(this Class @this, string name, Func<Method, Method> initializer = null)
        {
            return @this.WithAsyncMethod(name, typeof(T).ToBuilder(), initializer);
        }

        public static Class WithEvent<T>(this Class @this, string name, Func<Event, Event> initializer = null)
        {
            return @this.WithEvent(name, typeof(T).ToBuilder(), initializer);
        }

        public static Class WithField<T>(this Class @this, string name, Func<Field, Field> initializer = null)
        {
            return @this.WithField(name, typeof(T).ToBuilder(), initializer);
        }

        public static Class WithProperty<T>(this Class @this, string name, Func<Property, Property> initializer = null)
        {
            return @this.WithProperty(name, typeof(T).ToBuilder(), initializer);
        }

        public static Class WithInterface<T>(this Class @this)
        {
            return @this.WithInterface(typeof(T).ToBuilder());
        }

        public static Class WithParent<T>(this Class @this)
        {
            return @this.WithParent(typeof(T).ToBuilder());
        }
    }
}
