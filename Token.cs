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
    }
}