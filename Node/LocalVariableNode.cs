using System;
using System.Linq;

namespace mplc
{
    class LocalVariableNode : Node
    {
        public LocalVariable LocalVariable { get; set; }

        public override void Parse(Context context)
        {
            var token = context.Expect(Tokenizer.TokenKind.IDENTIFIER);
            if (!context.FindLocalVariable(token, out LocalVariable lvar))
            {
                var offset = context.LocalVariables.Last().Offset;
                lvar = new LocalVariable(token, offset + 8);
                context.LocalVariables.Add(lvar);
            }
            this.LocalVariable = lvar;
        }

        public override void Accept(AssemblyGenerateVisitor v)
        {
            v.Visit(this);
        }
    }
}