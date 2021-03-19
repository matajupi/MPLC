using System;
using System.Collections.Generic;
using System.IO;

namespace mplc
{
    class AssemblyGenerator
    {
        public AssemblyGenerateVisitor Visitor { get; set; }

        public AssemblyGenerator(AssemblyGenerateVisitor visitor)
            => this.Visitor = visitor;

        public void Add(string line) => Visitor.Add(line);

        public void Generate(Node parsedNode)
        {
            this.Visitor.Initialize();
            parsedNode.Accept(this.Visitor);
        }

        public void OutputFile(string path)
        {
            using (var sw = new StreamWriter(path))
            {
                foreach (var line in this.Visitor.Code)
                    sw.WriteLine(line);
            }
        }

        public void PutAll() // To Debug
        {
            foreach (var line in this.Visitor.Code)
                Console.WriteLine(line);
        }
    }
}