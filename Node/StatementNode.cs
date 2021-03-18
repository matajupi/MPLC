using System;

namespace mplc
{
    class StatementNode : Node
    {
        public Node ExpressionNode { get; set; }

        public override void Parse(Context context)
        {
            this.ExpressionNode = new ExpressionNode();
            this.ExpressionNode.Parse(context);
            context.Expect(Tokenizer.TokenKind.SEMICOLON);
        }

        public override void Accept(AssemblyGenerateVisitor v)
        {
            v.Visit(this);
        }
    }
}