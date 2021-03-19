using System;

namespace mplc
{
    class AssemblyGeneratorFactory
    {
        public AssemblyGenerator Create<T>() where T : AssemblyGenerateVisitor, new()
        {
            var visitor = new T();
            var generator = new AssemblyGenerator(visitor);
            return generator;
        }
    }
}