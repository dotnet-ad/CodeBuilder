using System;
namespace CodeBuilder
{
    public static class Modules
    {
        public static readonly Module System = new Module("System");

        public static readonly Module Threading = new Module("System.Threading");

        public static readonly Module ThreadingTasks = new Module("System.Threading.Tasks");

        public static readonly Module CollectionsGeneric = new Module("System.Collections.Generic");
    }
}
