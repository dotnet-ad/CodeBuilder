using System;
namespace CodeBuilder
{
    public interface ICodeGenerator
    {
        string Generate(Module module);
    }
}
