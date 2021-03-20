using System;

namespace mplc
{
    class ForNode : Node
    {
        public Node InitializeNode { get; set; }
        public Node ConditionNode { get; set; }
        public Node UpdateNode { get; set; }
        public Node StatementNode { get; set; }
        
        public override void Parse(Context context)
        {
            context.Expect(Tokenizer.TokenKind.FOR);
            context.Expect(Tokenizer.TokenKind.LEFT_PAREN);
            if (!context.Consume(Tokenizer.TokenKind.SEMICOLON))
            {
                this.InitializeNode = new ExpressionNode();
                this.InitializeNode.Parse(context);
                context.Expect(Tokenizer.TokenKind.SEMICOLON);
            }
            if (!context.Consume(Tokenizer.TokenKind.SEMICOLON))
            {
                this.ConditionNode = new ExpressionNode();
                this.ConditionNode.Parse(context);
                context.Expect(Tokenizer.TokenKind.SEMICOLON);
            }
            if (!context.Consume(Tokenizer.TokenKind.RIGHT_PAREN))
            {
                this.UpdateNode = new ExpressionNode();
                this.UpdateNode.Parse(context);
                context.Expect(Tokenizer.TokenKind.RIGHT_PAREN);
            }
            this.StatementNode = new StatementNode();
            this.StatementNode.Parse(context);
        }

        public override void Accept(AssemblyGenerateVisitor v)
        {
            v.Visit(this);
        }
    }
}