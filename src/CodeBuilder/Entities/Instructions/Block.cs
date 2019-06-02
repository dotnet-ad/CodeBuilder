using System;
namespace CodeBuilder
{
    public class Block : IInstruction
    {
        public Block(params IInstruction[] instructions)
        {
            this.Instructions = instructions ?? new IInstruction[0];
        }

        public IInstruction[] Instructions { get; }
    }
}
