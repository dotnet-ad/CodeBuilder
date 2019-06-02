namespace CodeBuilder
{
    public class Constructor
    {
        public Constructor(string documentation = null, AccessModifier access = AccessModifier.Public, Parameter[] parameters = null, Body body = null)
        {
            this.Documentation = documentation;
            this.Parameters = parameters ?? new Parameter[0];
            this.Body = body;
            this.Access = access;
        }

        public AccessModifier Access { get; }

        public string Documentation { get; }

        public Parameter[] Parameters { get; }

        public Body Body { get; }
    }
}
