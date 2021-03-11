using System;
using System.Collections.Generic;
using System.IO;

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
            var node = new AddNode();
            node.Parse(context);
            var asm = new AssemblyCode();
            asm.Add("main:");
            node.Generate(asm);
            asm.Add("    pop rax");
            asm.Add("    ret");
            asm.PutAll();
        }
    }
}
