using System;

namespace mplc
{
    class TokenizeException : Exception
    {
        public TokenizeException(string message) : base(message) { }
    }
}