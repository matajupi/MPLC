using System;

namespace mplc
{
    class PrimaryNode : Node
    {
        private Node Node;

        public override void Parse(Context context)
        {
            if (context.Consume(Tokenizer.TokenKind.LEFT_PAREN))
            {
                this.Node = new AdditionNode();
                this.Node.Parse(context);
                context.Expect(Tokenizer.TokenKind.RIGHT_PAREN);
            }
            else
            {
                this.Node = new NumberNode();
                this.Node.Parse(context);
            }
        }

        public override void Generate(AssemblyCode asm)
        {
            this.Node.Generate(asm);
        }
    }
}