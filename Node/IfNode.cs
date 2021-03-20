using System;

namespace mplc
{
    class IfNode : Node
    {
        public Node ConditionNode { get; set; }
        public Node StatementNode { get; set; }
        public Node ElseNode { get; set; }

        public override void Parse(Context context)
        {
            context.Expect(Tokenizer.TokenKind.IF);
            context.Expect(Tokenizer.TokenKind.LEFT_PAREN);
            this.ConditionNode = new ExpressionNode();
            this.ConditionNode.Parse(context);
            context.Expect(Tokenizer.TokenKind.RIGHT_PAREN);
            this.StatementNode = new StatementNode();
            this.StatementNode.Parse(context);
            var token = context.GetTokenOrDefaultAt(context.CurrentTokenIndex);
            if (token != default && token.TokenKind == Tokenizer.TokenKind.ELSE)
            {
                this.ElseNode = new ElseNode();
                this.ElseNode.Parse(context);
            }
        }

        public override void Accept(AssemblyGenerateVisitor v)
        {
            v.Visit(this);
        }
    }
}