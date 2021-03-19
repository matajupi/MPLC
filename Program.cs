using System;
using System.Collections.Generic;

namespace mplc
{
    class Program
    {
        static void Main(string[] args)
        {
            // TODO Later: コマンド解析処理
            if (args.Length != 1)
            {
                CompileError.Error("The number of command line argument must be 1.", true);
            }

            // TODO Later: ソースファイルのInput

            var code = args[0];
            var context = new Context(code);
            var node = new ProgramNode();
            node.Parse(context);

            var gen = new AssemblyGeneratorFactory().Create<X8664GenerateVisitor>();

            gen.Generate(node);

            // Epilogue
            gen.Add("    pop rax");
            gen.Add("    mov rsp, rbp");
            gen.Add("    pop rbp");
            gen.Add("    ret");

            gen.PutAll();
        }
    }
}
