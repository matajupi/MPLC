using System;

namespace mplc
{
    class WhileNode : Node
    {
        public Node ConditionNode { get; set; }
        public Node StatementNode { get; set; }

        public override void Parse(Context context)
        {
            context.Expect(Tokenizer.TokenKind.WHILE);
            context.Expect(Tokenizer.TokenKind.LEFT_PAREN);
            this.ConditionNode = new ExpressionNode();
            this.ConditionNode.Parse(context);
            context.Expect(Tokenizer.TokenKind.RIGHT_PAREN);
            this.StatementNode = new StatementNode();
            this.StatementNode.Parse(context);
        }

        public override void Accept(AssemblyGenerateVisitor v)
        {
            v.Visit(this);
        }
    }
}