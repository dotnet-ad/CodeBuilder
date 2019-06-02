namespace CodeBuilder
{
    public static class TypeExtensions
    {
        public static IType ToBuilder(this System.Type clr)
        {
            var module = new Module(clr.Namespace);
            return new Type(module, clr.Name);
        }
    }
}
