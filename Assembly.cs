using System;
using System.Collections.Generic;

namespace mplc
{
    class Assembly
    {
        public List<string> Code { get; private set; }

        public Assembly()
        {
            this.Code = new List<string>();
            this.Code.Add(".intel_syntax noprefix");
            this.Code.Add(".globl main");
        }
    }
}