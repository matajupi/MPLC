using System;

namespace mplc
{
    class AssignNode : Node
    {
        public Node LeftSide { get; set; }
        public Node RightSide { get; set; }

        public override void Parse(Context context)
        {
            this.LeftSide = new EqualityNode();
            this.LeftSide.Parse(context);
            if (context.Consume(Tokenizer.TokenKind.EQUAL))
            {
                this.RightSide = new EqualityNode();
                this.RightSide.Parse(context);
            }
        }

        public override void Accept(AssemblyGenerateVisitor v)
        {
            v.Visit(this);
        }
    }
}