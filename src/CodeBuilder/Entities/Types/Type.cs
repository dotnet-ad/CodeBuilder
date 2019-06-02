namespace CodeBuilder
{
    public class Type : BaseType
    {
        public Type(Module module, string name) : base(name)
        {
            this.Module = module;
        }

        public Module Module { get; }

        public string Fullname => $"{this.Module.Name}.{this.Name}";

        public override string ToString()
        {
            return this.Fullname;
        }
    }
}
