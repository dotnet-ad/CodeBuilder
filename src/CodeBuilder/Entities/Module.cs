namespace CodeBuilder
{
    public class Module
    {
        public Module(string name, IType[] types = null, Module[] imports = null)
        {
            this.Name = name;
            this.Types = types ?? new IType[0];
            this.Imports = imports ?? new Module[0];
        }

        public string Name { get; }

        public IType[] Types { get; }

        public Module[] Imports { get; }
    }
}
