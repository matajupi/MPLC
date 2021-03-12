using System;
using System.Collections.Generic;

namespace mplc
{
    class MultiplicationNode : Node
    {
        public Node LeftSide { get; set; }
        public List<Tuple<Tokenizer.TokenKind, Node>> RightSides { get; set; }

        public MultiplicationNode()
        => this.RightSides = new List<Tuple<Tokenizer.TokenKind, Node>>();

        public override void Parse(Context context)
        {
            this.LeftSide = new UnaryNode();
            this.LeftSide.Parse(context);

            Token token;
            while (context.Consume(Tokenizer.TokenKind.ASTERISK, out token)
                || context.Consume(Tokenizer.TokenKind.SLASH, out token))
            {
                var node = new UnaryNode();
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

                asm.BinaryOperation(() => 
                {
                    switch (pair.Item1)
                    {
                        case Tokenizer.TokenKind.ASTERISK:
                        asm.Add("    imul rax, rdi");
                        break;
                        case Tokenizer.TokenKind.SLASH:
                        asm.Add("    cqo");
                        asm.Add("    idiv rdi");
                        break;
                    }
                });
            }
        }
    }
}