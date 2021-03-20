using System;

namespace mplc
{
    class StatementNode : Node
    {
        public Node Node { get; set; }

        public override void Parse(Context context)
        {
            var token = context.GetTokenOrDefaultAt(context.CurrentTokenIndex);
            if (token != default && token.TokenKind == Tokenizer.TokenKind.RETURN)
            {
                this.Node = new ReturnNode();
                this.Node.Parse(context);
            }
            else
            {
                this.Node = new ExpressionNode();
                this.Node.Parse(context);
            }
            context.Expect(Tokenizer.TokenKind.SEMICOLON);
        }

        public override void Accept(AssemblyGenerateVisitor v)
        {
            v.Visit(this);
        }
    }
}