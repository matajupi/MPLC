using System;
using System.Linq;

namespace mplc
{
    class LocalVariableNode : Node
    {
        public LocalVariable LocalVariable { get; set; }
        public bool IsDefinition { get; set; }

        public override void Parse(Context context)
        {
            var token = context.Expect(Tokenizer.TokenKind.IDENTIFIER);
            this.Parse(context, token);
        }

        public void Parse(Context context, Token token)
        {
            if (context.FindLocalVariable(token, out LocalVariable v))
                this.IsDefinition = false;
            else
            {
                var offset = context.LocalVariables.Last().Offset;
                v = new LocalVariable(token, offset + 8);
                context.LocalVariables.Add(v);
            }
            this.LocalVariable = v;
        }

        public override void Accept(AssemblyGenerateVisitor v)
        {
            v.Visit(this);
        }
    }
}