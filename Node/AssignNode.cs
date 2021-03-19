using System;

namespace mplc
{
    class AssignNode : Node
    {
        public Node LeftSide { get; set; }
        public Node RightSide { get; set; }

        public override void Parse(Context context)
        {
            var token = context.GetTokenOrDefaultAt(context.CurrentTokenIndex + 1);
            if (token != default && token.TokenKind == Tokenizer.TokenKind.EQUAL)
            {
                this.LeftSide = new LocalVariableNode();
                this.LeftSide.Parse(context);
                context.Expect(Tokenizer.TokenKind.EQUAL);
                this.RightSide = new EqualityNode();
                this.RightSide.Parse(context);
                return;
            }
            this.LeftSide = new EqualityNode();
            this.LeftSide.Parse(context);
        }

        public override void Accept(AssemblyGenerateVisitor v)
        {
            v.Visit(this);
        }
    }
}