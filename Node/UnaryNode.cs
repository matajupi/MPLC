using System;
using System.Collections.Generic;

namespace mplc
{
    class UnaryNode : Node
    {
        public Node PrimaryNode { get; set; }

        public override void Parse(Context context)
        {
            if (context.Consume(Tokenizer.TokenKind.MINUS))
            {
                var subZero = new AdditionNode();
                var zero = new NumericLiteralNode();
                zero.Number = 0;
                subZero.LeftSide = zero;
                var right = new PrimaryNode();
                right.Parse(context);
                subZero.RightSides.Add(
                    new Tuple<Tokenizer.TokenKind, Node>(Tokenizer.TokenKind.MINUS, right)
                );
                this.PrimaryNode = subZero;
                return;
            }
            context.Consume(Tokenizer.TokenKind.PLUS);
            this.PrimaryNode = new PrimaryNode();
            this.PrimaryNode.Parse(context);
        }

        public override void Accept(AssemblyGenerateVisitor v)
        {
            v.Visit(this);
        }
    }
}