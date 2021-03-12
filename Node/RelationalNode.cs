using System;

namespace mplc
{
    class RelationalNode : Node
    {
        public Node LeftSide { get; set; }
        public Tokenizer.TokenKind Operator { get; set; }
        public Node RightSide { get; set; }

        public override void Parse(Context context)
        {
            this.LeftSide = new AdditionNode();
            this.LeftSide.Parse(context);
            Token token;
            if (context.Consume(Tokenizer.TokenKind.LESS, out token)
            || context.Consume(Tokenizer.TokenKind.LESS_EQUAL, out token))
            {
                this.Operator = token.TokenKind;
                this.RightSide = new AdditionNode();
                this.RightSide.Parse(context);
            }
            else if (context.Consume(Tokenizer.TokenKind.GREATER, out token))
            {
                this.Operator = Tokenizer.TokenKind.LESS;
                this.RightSide = this.LeftSide;
                this.LeftSide = new AdditionNode();
                this.LeftSide.Parse(context);
            }
            else if (context.Consume(Tokenizer.TokenKind.GREATER_EQUAL, out token))
            {
                this.Operator = Tokenizer.TokenKind.LESS_EQUAL;
                this.RightSide = this.LeftSide;
                this.LeftSide = new AdditionNode();
                this.LeftSide.Parse(context);
            }
        }

        public override void Generate(AssemblyCode asm)
        {
            this.LeftSide.Generate(asm);
            if (this.RightSide != default && this.Operator != default)
            {
                this.RightSide.Generate(asm);

                asm.BinaryOperation(() => 
                {
                    asm.Add("    cmp rax, rdi");
                    switch (this.Operator)
                    {
                        case Tokenizer.TokenKind.LESS:
                        asm.Add("    setl al");
                        break;
                        case Tokenizer.TokenKind.LESS_EQUAL:
                        asm.Add("    setle al");
                        break;
                    }
                    asm.Add("    movzb rax, al");
                });
            }
        }
    }
}