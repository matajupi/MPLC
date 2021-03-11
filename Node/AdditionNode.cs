using System;
using System.Collections.Generic;

namespace mplc
{
    class AdditionNode : Node
    {
        private Node LeftSide;
        private List<Tuple<Tokenizer.TokenKind, Node>> RightSides;

        public override void Parse(Context context)
        {
            this.LeftSide = new MultiplicationNode();
            this.LeftSide.Parse(context);

            this.RightSides = new List<Tuple<Tokenizer.TokenKind, Node>>();
            Token token;
            while (context.Consume(Tokenizer.TokenKind.PLUS, out token)
                || context.Consume(Tokenizer.TokenKind.MINUS, out token))
            {
                var node = new MultiplicationNode();
                node.Parse(context);
                this.RightSides.Add(
                    new Tuple<Tokenizer.TokenKind, Node>(token.TokenKind, node)
                );
            }
        }

        public override void Generate(AssemblyCode asm)
        {
            this.LeftSide.Generate(asm);

            foreach (var pair in this.RightSides)
            {
                pair.Item2.Generate(asm);

                asm.Add("    pop rdi");
                asm.Add("    pop rax");

                switch (pair.Item1)
                {
                    case Tokenizer.TokenKind.PLUS:
                    asm.Add("    add rax, rdi");
                    break;
                    case Tokenizer.TokenKind.MINUS:
                    asm.Add("    sub rax, rdi");
                    break;
                }
                asm.Add("    push rax");
            }
        }
    }
}