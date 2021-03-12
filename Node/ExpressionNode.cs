using System;

namespace mplc
{
    class ExpressionNode : Node
    {
        public Node Node { get; set; }

        public override void Parse(Context context)
        {
            this.Node = new EqualityNode();
            this.Node.Parse(context);
        }

        public override void Generate(AssemblyCode asm)
        {
            this.Node.Generate(asm);
        }
    }
}