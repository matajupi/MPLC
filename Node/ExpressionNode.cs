using System;

namespace mplc
{
    class ExpressionNode : Node
    {
        public Node AssignNode { get; set; }

        public override void Parse(Context context)
        {
            this.AssignNode = new AssignNode();
            this.AssignNode.Parse(context);
        }

        public override void Accept(AssemblyGenerateVisitor v)
        {
            v.Visit(this);
        }
    }
}