using System;
namespace CodeBuilder
{
    public class Statement : IInstruction
    {
        public Statement(string content)
        {
            this.Content = content;
        }

        public string Content { get; }
    }
}
