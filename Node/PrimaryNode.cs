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
            if (context.Consume(Tokenizer.TokenKind.IDENTIFIER, out Token token))
            {
                var lvar = new LocalVariableNode();
                lvar.Parse(context, token);
                this.Node = lvar;
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