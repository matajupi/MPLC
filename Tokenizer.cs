using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
            EQUAL,
            RETURN,
            IF,
            ELSE,
            WHILE,
            FOR,
            NUMERIC,
            PLUS,
            MINUS,
            ASTERISK,
            SLASH,
            LEFT_PAREN,
            RIGHT_PAREN,
            LEFT_CURLY_BRACE,
            RIGHT_CURLY_BRACE,
            LESS,
            GREATER,
            EQUAL_EQUAL,
            EXCLAMATION_EQUAL,
            LESS_EQUAL,
            GREATER_EQUAL,
            IDENTIFIER,
            SEMICOLON,
            EOF,
        }

        public static IReadOnlyDictionary<TokenKind, string> TokenString = new Dictionary<TokenKind, string>()
        {
            { TokenKind.EQUAL, "=" },
            { TokenKind.RETURN, "return"},
            { TokenKind.IF, "if" },
            { TokenKind.ELSE, "else" },
            { TokenKind.WHILE, "while" },
            { TokenKind.FOR, "for" },
            { TokenKind.NUMERIC , "numeric" },
            { TokenKind.PLUS, "+"},
            { TokenKind.MINUS, "-"},
            { TokenKind.ASTERISK, "*" },
            { TokenKind.SLASH, "/" },
            { TokenKind.LEFT_PAREN, "(" },
            { TokenKind.RIGHT_PAREN, ")"},
            { TokenKind.LEFT_CURLY_BRACE, "{" },
            { TokenKind.RIGHT_CURLY_BRACE, "}" },
            { TokenKind.LESS, "<" },
            { TokenKind.GREATER, ">" },
            { TokenKind.EQUAL_EQUAL, "==" },
            { TokenKind.EXCLAMATION_EQUAL, "!=" },
            { TokenKind.LESS_EQUAL, "<=" },
            { TokenKind.GREATER_EQUAL, ">=" },
            { TokenKind.IDENTIFIER, "identifier" },
            { TokenKind.SEMICOLON, ";" },
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
                // Muliti-letter punctuator
                if (this.Text.Length - p >= 2)
                {
                    var mlp = this.Text.Substring(p, 2);
                    if (ExistsKey(mlp))
                    {
                        var tk = ReverseReference(mlp);
                        tokens.Add(new Token(tk, mlp));
                        p += 2;
                        continue;
                    }
                }
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
                    var token = new Token(TokenKind.NUMERIC, this.Text.Substring(p, len));
                    tokens.Add(token);
                    p += len;
                    continue;
                }
                // Identifier or keyword
                if (this.IsAllowedCharacter(this.Text[p]))
                {
                    var builder = new StringBuilder();
                    for (; this.IsAllowedCharacter(this.Text[p]); p++)
                        builder.Append(this.Text[p]);
                    var word = builder.ToString();

                    // Keyword
                    if (ExistsKey(word))
                    {
                        var tk = ReverseReference(word);
                        tokens.Add(new Token(tk, word));
                        continue;
                    }
                    
                    // Identifier
                    var token = new Token(TokenKind.IDENTIFIER, word);
                    tokens.Add(token);
                    continue;
                }

                CompileError.Error("Couldn't tokenize.", true);
            }

            tokens.Add(new Token(TokenKind.EOF, "EOF"));
            return tokens.ToArray();
        }

        private bool IsAllowedCharacter(char c)
        => ('a' <= c && c <= 'z')
        || ('A' <= c && c <= 'Z')
        || ('0' <= c && c <= '9')
        || (c == '_');
        
        public Token NextToken()
        {
            var token = this.Tokens[this.Index];
            this.Index++;
            return token;
        }
    }
}
