using System;
using System.Collections.Generic;

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
            var tok = obj as Token;

            return tok != null 
            && this.TokenKind == tok.TokenKind
            && this.TokenString == tok.TokenString;
        }

        public override int GetHashCode()
        {
            return EqualityComparer<string>.Default.GetHashCode(this.TokenString) ^ (int)this.TokenKind;
        }
    }
}