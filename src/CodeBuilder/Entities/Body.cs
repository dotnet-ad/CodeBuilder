namespace CodeBuilder
{
    public class Body
    {
        public Body(IInstruction root = null)
        {
            this.Root = root;
        }

        public IInstruction Root { get; }

        public static Body Auto = new Body();

        public static Body None = new Body();
    }
}
