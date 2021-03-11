using System;
using System.Collections.Generic;

namespace mplc
{
    class AddNode : Node
    {
        private NumberNode LeftSide;
        private List<Tuple<Tokenizer.TokenKind, NumberNode>> RightSides;

        public override void Parse(Context context)
        {
            this.LeftSide = new NumberNode();
            this.LeftSide.Parse(context);

            this.RightSides = new List<Tuple<Tokenizer.TokenKind, NumberNode>>();
            Token token;
            while (context.Consume(Tokenizer.TokenKind.TK_PLUS, out token)
                || context.Consume(Tokenizer.TokenKind.TK_MINUS, out token))
            {
                var node = new NumberNode();
                node.Parse(context);
                this.RightSides.Add(new Tuple<Tokenizer.TokenKind, NumberNode>(token.TokenKind, node));
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
                    case Tokenizer.TokenKind.TK_PLUS:
                    asm.Add("    add rax, rdi");
                    break;
                    case Tokenizer.TokenKind.TK_MINUS:
                    asm.Add("    sub rax, rdi");
                    break;
                }
                asm.Add("    push rax");
            }
        }
    }
}