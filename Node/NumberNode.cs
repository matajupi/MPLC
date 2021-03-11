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

        public override void Generate(AssemblyCode asm)
        {
            asm.Add($"    push {this.Number}");
        }
    }
}