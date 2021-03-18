using System;

namespace mplc
{
    class LocalVariable
    {
        public Token Token { get; set; }
        public int Offset { get; set; }
        // TODO later: Add type

        public LocalVariable(Token token, int offset)
        {
            this.Token = token;
            this.Offset = offset;
        }
    }
}