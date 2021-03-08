using System;

namespace mplc
{
    // 名前は仮(IntegerNodeとかにする)
    class NumberNode : Node
    {
        private int Number;

        public override void Parse(Context context)
        {
            this.Number = context.ExpectNumber();
        }

        public override void Generate(Assembly asm)
        {
            asm.Code.Add("main:");
            asm.Code.Add($"    mov rax, {this.Number}");
            asm.Code.Add("    ret");
        }
    }
}