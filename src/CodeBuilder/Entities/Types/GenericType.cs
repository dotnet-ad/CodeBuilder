namespace CodeBuilder
{
    using System.Linq;

    public class GenericType : Type
    {
        public GenericType(Module module, string name, params IType[] parameters) : base(module,name)
        {
            this.Parameters = parameters;
        }

        public IType[] Parameters { get; }

        public override string ToString()
        {
            return base.ToString() + "<" + string.Join(", ", this.Parameters.Select(p => p.ToString())) + ">";
        }
    }
}
