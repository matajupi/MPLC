using System;
using System.Collections.Generic;
using System.IO;

namespace mplc
{
    class AssemblyCode
    {
        private List<string> Code;

        public AssemblyCode()
        {
            this.Code = new List<string>();
            this.Initialize();
        }

        private void Initialize()
        {
            this.Add(".intel_syntax noprefix");
            this.Add(".globl main");
        }

        public void Add(string line) => this.Code.Add(line);

        public void OutputFile(string path)
        {
            using (var sw = new StreamWriter(path))
            {
                foreach (var line in this.Code)
                    sw.WriteLine(line);
            }
        }

        public void PutAll() // To Debug
        {
            foreach (var line in this.Code)
                Console.WriteLine(line);
        }
    }
}