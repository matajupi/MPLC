using System;
using System.Collections.Generic;
using System.Linq;
using static mplc.Tokenizer;

namespace mplc
{
    class Context
    {
        public Tokenizer Tokenizer { get; private set; }
        public Token CurrentToken { get; private set; }
        public int CurrentTokenIndex { get { return this.Tokenizer.Index - 1; } }
        public List<LocalVariable> LocalVariables { get; set; }
        
        public Context(string code)
        {
            this.Tokenizer = new Tokenizer(code);
            this.CurrentToken = this.Tokenizer.NextToken();
            // TODO Later: Change Place
            this.LocalVariables = new List<LocalVariable>();
            this.LocalVariables.Add(new LocalVariable(new Token(TokenKind.IDENTIFIER, string.Empty), 0));
        }

        /// <summary>
        /// Consume one token.
        /// </summary>
        public void AdvanceToken()
        {
            if (!this.AtEOF())
                this.CurrentToken = this.Tokenizer.NextToken();
        }

        /// <summary>
        /// When the current token kind is the expected kind, it consumes one token and returns true.
        /// Otherwise it returns false.
        /// </summary>
        public bool Consume(TokenKind tokenKind)
        {
            if (this.CurrentToken.TokenKind != tokenKind)
                return false;

            this.AdvanceToken();
            return true;
        }

        /// <summary>
        /// When the current token kind is the expected kind, it consumes one token and
        /// sets the current token to the argument token, and returns true.
        /// Otherwise it returns false.
        /// </summary>
        public bool Consume(TokenKind tokenKind, out Token token)
        {
            if (this.CurrentToken == default || this.CurrentToken.TokenKind != tokenKind)
            {
                token = default;
                return false;
            }
            token = this.CurrentToken;
            this.AdvanceToken();
            return true;
        }

        /// <summary>
        /// When the current token kind is the expected kind, it consumes one token and returns that token.
        /// Otherwise it reports an error.
        /// </summary>
        public Token Expect(TokenKind tokenKind)
        {
            if (this.CurrentToken.TokenKind != tokenKind)
                CompileError.Error("Expect Error", true);
            var retval = this.CurrentToken;
            this.AdvanceToken();
            return retval;
        }

        /// <summary>
        /// When the current token is a number, it consumes one token and returns that number.
        /// Otherwise it reports an error.
        /// </summary>
        public int ExpectNumber()
        {
            if (this.CurrentToken.TokenKind != TokenKind.NUMERIC)
                CompileError.Error("Expect number", true);
            var val = int.Parse(this.CurrentToken.TokenString);
            this.AdvanceToken();
            return val;
        }

        /// <summary>
        /// Returns the token of the specified index, if it exists.
        /// Otherwise returns default.
        /// </summary>
        public Token GetTokenOrDefaultAt(int index) 
        {
            if (index < this.Tokenizer.Tokens.Length) return this.Tokenizer.Tokens[index];
            else return default;
        }

        /// <summary>
        /// Whether the current token is EOF.
        /// </summary>
        public bool AtEOF() => this.CurrentToken.TokenKind == TokenKind.EOF;

        /// <summary>
        /// If there is a "LocalVarible" object corresponding to the specified token, set that object to lvar
        /// and return true.
        /// Otherwise, set null to lvar and return false.
        /// </summary>
        public bool FindLocalVariable(Token token, out LocalVariable lvar)
        {
            foreach (var v in this.LocalVariables)
            {
                if (v.Token.Equals(token))
                {
                    lvar = v;
                    return true;
                }
            }
            lvar = null;
            return false;
        }
    }
}