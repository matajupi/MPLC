using System;

namespace mplc
{
    class MethodCallNode : Node
    {
        public Token IdentifierToken { get; set; }

        public override void Parse(Context context)
        {
            this.IdentifierToken = context.Expect(Tokenizer.TokenKind.IDENTIFIER);
            context.Expect(Tokenizer.TokenKind.LEFT_PAREN);
            context.Expect(Tokenizer.TokenKind.RIGHT_PAREN);
        }

        public override void Accept(AssemblyGenerateVisitor v)
        {
            v.Visit(this);
        }
    }
}