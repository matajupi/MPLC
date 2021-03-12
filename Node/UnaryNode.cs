using System;
using System.Collections.Generic;

namespace mplc
{
    class UnaryNode : Node
    {
        public Node Node;

        public override void Parse(Context context)
        {
            if (context.Consume(Tokenizer.TokenKind.MINUS))
            {
                var subZero = new AdditionNode();
                var zero = new NumberNode();
                zero.Number = 0;
                subZero.LeftSide = zero;
                var right = new PrimaryNode();
                right.Parse(context);
                subZero.RightSides.Add(
                    new Tuple<Tokenizer.TokenKind, Node>(Tokenizer.TokenKind.MINUS, right)
                );
                this.Node = subZero;
                return;
            }
            context.Consume(Tokenizer.TokenKind.PLUS);
            this.Node = new PrimaryNode();
            this.Node.Parse(context);
        }

        public override void Generate(AssemblyCode asm)
        {
            this.Node.Generate(asm);
        }
    }
}