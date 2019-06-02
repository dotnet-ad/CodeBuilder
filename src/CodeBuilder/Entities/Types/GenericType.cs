namespace CodeBuilder
{
    public class GenericType : Type
    {
        public GenericType(Module module, string name, params IType[] parameters) : base(module,name)
        {
            this.Parameters = parameters;
        }

        public IType[] Parameters { get; }
    }
}
