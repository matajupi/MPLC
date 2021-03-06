using System;

namespace mplc
{
    class ExpressionNode : Node
    {
        public Node Node { get; set; }

        public override void Parse(Context context)
        {
            this.Node = new AssignNode();
            this.Node.Parse(context);
        }

        public override void Accept(AssemblyGenerateVisitor v)
        {
            v.Visit(this);
        }
    }
}