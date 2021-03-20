using System;

namespace mplc
{
    class ElseNode : Node
    {
        public Node StatementNode { get; set; }

        public override void Parse(Context context)
        {
            context.Expect(Tokenizer.TokenKind.ELSE);
            this.StatementNode = new StatementNode();
            this.StatementNode.Parse(context);
        }

        public override void Accept(AssemblyGenerateVisitor v)
        {
            v.Visit(this);
        }
    }
}