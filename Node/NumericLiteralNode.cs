using System;

namespace mplc
{
    class NumericLiteralNode : Node
    {
        public int Number { get; set; }

        public override void Parse(Context context)
        {
            this.Number = context.ExpectNumber();
        }

        public override void Accept(AssemblyGenerateVisitor v)
        {
            v.Visit(this);
        }
    }
}