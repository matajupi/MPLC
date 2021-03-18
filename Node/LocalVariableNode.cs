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
            context.Consume(Tokenizer.TokenKind.IDENTIFIER, out Token token);
            this.Parse(context, token);
        }

        public void Parse(Context context, Token token)
        {
            this.IsDefinition = context.FindLocalVariable(token, out LocalVariable v);
            
            if (this.IsDefinition)
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