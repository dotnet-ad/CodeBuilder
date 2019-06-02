namespace CodeBuilder
{
    public class Constructor
    {
        public Constructor(string documentation = null, AccessModifier access = AccessModifier.Public, Parameter[] parameters = null, Body body = null, string[] baseInitializers = null)
        {
            this.Documentation = documentation;
            this.Parameters = parameters ?? new Parameter[0];
            this.BaseInitializers = baseInitializers ?? new string[0];
            this.Body = body;
            this.Access = access;
        }

        public AccessModifier Access { get; }

        public string Documentation { get; }

        public Parameter[] Parameters { get; }

        public string[] BaseInitializers { get; }

        public Body Body { get; }
    }
}
