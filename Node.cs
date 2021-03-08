using System;

namespace mplc
{
    abstract class Node : AssemblyGenerator
    {
        public abstract void Parse(Context context);
    }
}