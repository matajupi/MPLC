using System;

namespace mplc
{
    class PrimaryNode : Node
    {
        public Node Node { get; set; }

        public override void Parse(Context context)
        {
            if (context.Consume(Tokenizer.TokenKind.LEFT_PAREN))
            {
                this.Node = new ExpressionNode();
                this.Node.Parse(context);
                context.Expect(Tokenizer.TokenKind.RIGHT_PAREN);
                return;
            }
            var token = context.GetTokenOrDefaultAt(context.CurrentTokenIndex);
            if (token != default && token.TokenKind == Tokenizer.TokenKind.IDENTIFIER)
            {
                this.Node = new LocalVariableNode();
                this.Node.Parse(context);
                return;
            }
            this.Node = new NumericLiteralNode();
            this.Node.Parse(context);
        }

        public override void Accept(AssemblyGenerateVisitor v)
        {
            v.Visit(this);
        }
    }
}