using System;

namespace mplc
{
    interface IAssemblyGeneratorAcceptor
    {
        /// <summary>
        /// Accept assmbly generate visitor.
        /// </summary>
        /// <param name="visitor"></param>
        void Accept(AssemblyGenerateVisitor visitor);
    }
}