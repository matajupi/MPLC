using System;
using System.Collections.Generic;
using System.Linq;
using static mplc.Tokenizer;

namespace mplc
{
    class Context
    {
        private Tokenizer Tokenizer;
        public Token CurrentToken { get; private set; }

        public List<LocalVariable> LocalVariables { get; set; }
        
        public Context(string code)
        {
            this.Tokenizer = new Tokenizer(code);
            this.CurrentToken = this.Tokenizer.NextToken();
            // TODO Later: Change Place
            this.LocalVariables = new List<LocalVariable>();
            this.LocalVariables.Add(new LocalVariable(null, 0));
        }

        /// <summary>
        /// Return current token and consume one.
        /// </summary>
        public Token AdvanceToken()
        {
            var retval = this.CurrentToken;
            if (this.Tokenizer.HasMoreToken())
                this.CurrentToken = this.Tokenizer.NextToken();
            else
                this.CurrentToken = default;
            return retval;
        }

        /// <summary>
        /// When the next token kind is the expected kind, it consumes one token and returns true.
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
        /// When the next token kind is the expected kind, it consumes one token and
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
            token = this.AdvanceToken();
            return true;
        }

        /// <summary>
        /// When the next token kind is the expected kind, it consumes one token.
        /// Otherwise it reports an error.
        /// </summary>
        public Token Expect(TokenKind tokenKind)
        {
            if (this.CurrentToken.TokenKind != tokenKind)
                CompileError.Error("Expect Error", true);
            return this.AdvanceToken();
        }

        /// <summary>
        /// When the next token is a number, it reads one token and returns that number.
        /// Otherwise it reports an error.
        /// </summary>
        public int ExpectNumber()
        {
            if (this.CurrentToken.TokenKind != TokenKind.NUMERIC)
                CompileError.Error("Expect number", true);
            var val = int.Parse(this.AdvanceToken().TokenString);
            return val;
        }

        public bool AtEOF() => this.CurrentToken.TokenKind == TokenKind.EOF;

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