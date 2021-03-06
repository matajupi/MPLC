using System;

namespace mplc
{
    class EqualityNode : Node
    {
        public Node LeftSide { get; set; }
        public Tokenizer.TokenKind Operator { get; set; }
        public Node RightSide { get; set; }

        public override void Parse(Context context)
        {
            this.LeftSide = new RelationalNode();
            this.LeftSide.Parse(context);
            Token token;
            if (context.Consume(Tokenizer.TokenKind.EQUAL_EQUAL, out token)
            || context.Consume(Tokenizer.TokenKind.EXCLAMATION_EQUAL, out token))
            {
                this.Operator = token.TokenKind;
                this.RightSide = new RelationalNode();
                this.RightSide.Parse(context);
            }
        }

        public override void Accept(AssemblyGenerateVisitor v)
        {
            v.Visit(this);
        }
    }
}