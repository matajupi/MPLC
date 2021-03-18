using System;

namespace mplc
{
    class Token
    {
        public Tokenizer.TokenKind TokenKind { get; set; }
        public string TokenString { get; set; }

        public Token(Tokenizer.TokenKind tk, string ts)
        {
            this.TokenKind = tk;
            this.TokenString = ts;
        }

        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            if (obj is Token)
            {
                var tok = obj as Token;
                if (this.TokenKind == tok.TokenKind 
                && this.TokenString == tok.TokenString) return true;
            }
            return false;
        }
    }
}