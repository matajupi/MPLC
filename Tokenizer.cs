using System;
using System.Collections.Generic;
using System.Linq;

namespace mplc
{
    /// <summary>
    /// Tokenize the passed text.
    /// </summary>
    class Tokenizer
    {
        /// <summary>
        /// Token identifier.
        /// </summary>
        public enum TokenKind
        {
            TK_EQUAL,
            TK_RETURN,
            TK_NUM,
            TK_PLUS,
            TK_MINUS,
            TK_EOF,
        }

        public static IReadOnlyDictionary<TokenKind, string> TokenString = new Dictionary<TokenKind, string>()
        {
            { TokenKind.TK_EQUAL, "=" },
            { TokenKind.TK_RETURN, "return"},
            { TokenKind.TK_NUM , "num" },
            { TokenKind.TK_PLUS, "+"},
            { TokenKind.TK_MINUS, "-"},
        };

        public static char[] SkipChars = new char[] { ' ', '\n', '\t', '\r', '\f' };

        /// <summary>
        /// Checks if there is a specified value in TokenString.
        /// </summary>
        public static bool ExistsKey(string ident)
        {
            return TokenString.Where(x => x.Value == ident).ToList().Count > 0;
        }

        /// <summary>
        /// Reverse reference to TokenString and return its key.
        /// </summary>
        public static TokenKind ReverseReference(string ident)
        {
            return TokenString.FirstOrDefault(x => x.Value == ident).Key;
        }

        /// <summary>
        /// Returns the length of the number following the given string.
        /// </summary>
        public static int DigitLength(string t)
        {
            int len;
            for (len = 0; t.Length > len && char.IsDigit(t[len]); len++) { }
            return len;
        }


        public string Text { get; private set; }
        public Token[] Tokens { get; private set; }
        public int Index { get; private set; }

        public Tokenizer(string text)
        {
            this.Text = text;
            this.Tokens = this.Tokenize();
            this.Index = 0;
        }

        private Token[] Tokenize()
        {
            var tokens = new List<Token>();
            for (var p = 0; this.Text.Length > p; )
            {
                // Skip character
                if (SkipChars.Contains(this.Text[p]))
                {
                    p++;
                    continue;
                }
                // // Muliti-letter punctuator
                // var mlp = this.Text.Substring(p, 2);
                // if (ExistsKey(mlp))
                // {
                //    var tk = ReverseReference(mlp);
                //    tokens.Add(new Tuple<TokenKind, string>(tk, mlp));
                //    p += 2;
                //    continue;
                // }
                // Single-letter punctuator
                if (ExistsKey(this.Text[p].ToString()))
                {
                   var tk = ReverseReference(this.Text[p].ToString());
                   tokens.Add(new Token(tk, this.Text[p].ToString()));
                   p++;
                   continue;
                }
                // Integer literal
                var len = DigitLength(this.Text.Substring(p));
                if (len > 0)
                {
                    var token = new Token(TokenKind.TK_NUM, this.Text.Substring(p, len));
                    tokens.Add(token);
                    p += len;
                    continue;
                }

                CompileError.Error("Couldn't tokenize.", true);
            }

            tokens.Add(new Token(TokenKind.TK_EOF, "EOF"));
            return tokens.ToArray();
        }
        
        public Token NextToken()
        {
            var token = this.Tokens[this.Index];
            this.Index++;
            return token;
        }

        public bool HasMoreToken() => this.Tokens.Length > this.Index;
    }
}
