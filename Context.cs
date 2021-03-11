using System;
using System.Collections.Generic;
using static mplc.Tokenizer;

namespace mplc
{
    class Context
    {
        private Tokenizer Tokenizer;
        public Token CurrentToken { get; private set; }
        
        public Context(string code)
        {
            this.Tokenizer = new Tokenizer(code);
            this.CurrentToken = this.Tokenizer.NextToken();
        }

        /// <summary>
        /// Return current token and consume one.
        /// </summary>
        public Token NextToken()
        {
            var retval = this.CurrentToken;
            if (this.Tokenizer.HasMoreToken())
                this.CurrentToken = this.Tokenizer.NextToken();
            else
                this.CurrentToken = null;
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

            this.NextToken();
            return true;
        }

        /// <summary>
        /// When the next token kind is the expected kind, it consumes one token and
        /// sets the current token to the argument token, and returns true.
        /// Otherwise it returns false.
        /// </summary>
        public bool Consume(TokenKind tokenKind, out Token token)
        {
            if (this.CurrentToken == null || this.CurrentToken.TokenKind != tokenKind)
            {
                token = null;
                return false;
            }
            token = this.NextToken();
            return true;
        }

        /// <summary>
        /// When the next token kind is the expected kind, it consumes one token.
        /// Otherwise it reports an error.
        /// </summary>
        public void Expect(TokenKind tokenKind)
        {
            if (this.CurrentToken.TokenKind != tokenKind)
                CompileError.Error("Expect Error", true);
            this.NextToken();
        }

        /// <summary>
        /// When the next token is a number, it reads one token and returns that number.
        /// Otherwise it reports an error.
        /// </summary>
        public int ExpectNumber()
        {
            if (this.CurrentToken.TokenKind != TokenKind.NUM)
                CompileError.Error("Expect number", true);
            var val = int.Parse(this.NextToken().TokenString);
            return val;
        }
    }
}