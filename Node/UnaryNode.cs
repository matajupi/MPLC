using System;

namespace mplc
{
    class UnaryNode : Node
    {
        private Node Node;
        private bool IsMinus;

        public override void Parse(Context context)
        {
            this.IsMinus = context.Consume(Tokenizer.TokenKind.MINUS);
            if (!this.IsMinus)
                context.Consume(Tokenizer.TokenKind.PLUS);
            this.Node = new PrimaryNode();
            this.Node.Parse(context);
        }

        public override void Generate(AssemblyCode asm)
        {
            this.Node.Generate(asm);

            if (this.IsMinus)
            {
                asm.Add("    pop rdi");
                asm.Add("    mov rax, 0");
                asm.Add("    sub rax, rdi");
                asm.Add("    push rax");
            }
        }
    }
}