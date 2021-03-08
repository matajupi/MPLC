using System;

namespace mplc
{
    class ParseException : Exception
    {
        public ParseException(string message) : base(message) { }
    }
}