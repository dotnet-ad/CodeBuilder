using System;
using System.Linq;

namespace CodeBuilder
{
    // TODO create a generator for creating this as soon as the generator is ready
    public static class InterfaceExtensions
    {

        public static Interface WithName(this Interface @this, string value) => new Interface(value,
                                                                                   documentation: @this.Documentation,
                                                                                   access: @this.Access,
                                                                                   events: @this.Events,
                                                                                   properties: @this.Properties,
                                                                                   methods: @this.Methods,
                                                                                   interfaces: @this.Interfaces);

        public static Interface WithAccess(this Interface @this, AccessModifier value) => new Interface(@this.Name,
                                                                                   documentation: @this.Documentation,
                                                                                   access: value,
                                                                                   events: @this.Events,
                                                                                   properties: @this.Properties,
                                                                                   methods: @this.Methods,
                                                                                   interfaces: @this.Interfaces);


        public static Interface WithProperty(this Interface @this, Property value) => new Interface(@this.Name,
                                                                                   documentation: @this.Documentation,
                                                                                   access: @this.Access,
                                                                                   events: @this.Events,
                                                                                   properties: @this.Properties.Concat(new[] { value }).ToArray(),
                                                                                   methods: @this.Methods,
                                                                                                    interfaces: @this.Interfaces);


        public static Interface WithEvent(this Interface @this, Event value) => new Interface(@this.Name,
                                                                                   documentation: @this.Documentation,
                                                                                   access: @this.Access,
                                                                                   events: @this.Events.Concat(new[] { value }).ToArray(),
                                                                                   properties: @this.Properties,
                                                                                   methods: @this.Methods,
                                                                                   interfaces: @this.Interfaces);


        public static Interface WithMethod(this Interface @this, Method value) => new Interface(@this.Name,
                                                                                   documentation: @this.Documentation,
                                                                                   access: @this.Access,
                                                                                   events: @this.Events,
                                                                                   properties: @this.Properties,
                                                                                   methods: @this.Methods.Concat(new[] { value }).ToArray(),
                                                                                   interfaces: @this.Interfaces);


        public static Interface WithInterface(this Interface @this, IType value) => new Interface(@this.Name,
                                                                                   documentation: @this.Documentation,
                                                                                   access: @this.Access,
                                                                                   events: @this.Events,
                                                                                   properties: @this.Properties,
                                                                                   methods: @this.Methods,
                                                                                   interfaces: @this.Interfaces.Concat(new[] { value }).ToArray());

        public static Interface WithProperty(this Interface @this, string name, IType type, Func<Property, Property> initializer = null)
        {
            var property = new Property(name, type, getter: Body.Auto, setter: Body.Auto);
            if (initializer != null)
            {
                property = initializer(property);
            }
            return @this.WithProperty(property);
        }

        public static Interface WithMethod(this Interface @this, string name, IType returnType, Func<Method, Method> initializer = null)
        {
            var method = new Method(name, returnType: returnType);
            if (initializer != null)
            {
                method = initializer(method);
            }
            return @this.WithMethod(method);
        }

        public static Interface WithEvent(this Interface @this, string name, IType handlerType = null, Func<Event, Event> initializer = null)
        {
            var ev = new Event(name, handlerType: handlerType);
            if (initializer != null)
            {
                ev = initializer(ev);
            }
            return @this.WithEvent(ev);
        }

        public static Interface WithInterface<T>(this Interface @this)
        {
            return @this.WithInterface(typeof(T).ToBuilder());
        }

        public static Interface WithMethod<T>(this Interface @this, string name, Func<Method, Method> initializer = null)
        {
            return @this.WithMethod(name, typeof(T).ToBuilder(), initializer);
        }

        public static Interface WithEvent<T>(this Interface @this, string name, Func<Event, Event> initializer = null)
        {
            return @this.WithEvent(name, typeof(T).ToBuilder(), initializer);
        }

        public static Interface WithProperty<T>(this Interface @this, string name, Func<Property, Property> initializer = null)
        {
            return @this.WithProperty(name, typeof(T).ToBuilder(), initializer);
        }
    }
}
