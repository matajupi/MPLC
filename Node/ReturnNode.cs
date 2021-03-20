using System;

namespace mplc
{
    class ReturnNode : Node
    {
        public Node Node { get; set; }

        public override void Parse(Context context)
        {
            context.Expect(Tokenizer.TokenKind.RETURN);
            this.Node = new ExpressionNode();
            this.Node.Parse(context);
        }

        public override void Accept(AssemblyGenerateVisitor v)
        {
            v.Visit(this);
        }
    }
}