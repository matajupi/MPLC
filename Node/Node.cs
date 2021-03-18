using System;

namespace mplc
{
    abstract class Node : IAssemblyGeneratorAcceptor
    {
        public abstract void Parse(Context context);
        public abstract void Accept(AssemblyGenerateVisitor v);
    }
}