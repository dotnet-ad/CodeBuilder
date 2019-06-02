using System;
namespace CodeBuilder
{
    public class BaseType : IType
    {
        public BaseType(string name)
        {
            this.Name = name;
        }

        public string Name { get; }

        public override string ToString()
        {
            return this.Name;
        }
    }
}
