using System;

namespace mplc
{
    class StatementNode : Node
    {
        public Node Node { get; set; }

        public override void Parse(Context context)
        {
            var token = context.GetTokenOrDefaultAt(context.CurrentTokenIndex);
            switch (token.TokenKind)
            {
                case Tokenizer.TokenKind.RETURN:
                this.Node = new ReturnNode();
                this.Node.Parse(context);
                context.Expect(Tokenizer.TokenKind.SEMICOLON);
                break;
                case Tokenizer.TokenKind.IF:
                this.Node = new IfNode();
                this.Node.Parse(context);
                break;
                case Tokenizer.TokenKind.WHILE:
                this.Node = new WhileNode();
                this.Node.Parse(context);
                break;
                case Tokenizer.TokenKind.FOR:
                this.Node = new ForNode();
                this.Node.Parse(context);
                break;
                default:
                this.Node = new ExpressionNode();
                this.Node.Parse(context);
                context.Expect(Tokenizer.TokenKind.SEMICOLON);
                break;
            }
        }

        public override void Accept(AssemblyGenerateVisitor v)
        {
            v.Visit(this);
        }
    }
}