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
        /// Consume one token.
        /// </summary>
        public void AdvanceToken()
        {
            if (this.Tokenizer.HasMoreToken())
                this.CurrentToken = this.Tokenizer.NextToken();
            else
                this.CurrentToken = null;
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
            if (this.CurrentToken.TokenKind != tokenKind)
            {
                token = null;
                return false;
            }
            token = this.CurrentToken;
            this.AdvanceToken();
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
            this.AdvanceToken();
        }

        /// <summary>
        /// When the next token is a number, it reads one token and returns that number.
        /// Otherwise it reports an error.
        /// </summary>
        public int ExpectNumber()
        {
            if (this.CurrentToken.TokenKind != TokenKind.TK_NUM)
                CompileError.Error("Expect number", true);
            var val = int.Parse(this.CurrentToken.TokenString);
            this.AdvanceToken();
            return val;
        }
    }
}