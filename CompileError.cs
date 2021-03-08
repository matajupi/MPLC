using System;

namespace mplc
{
    class CompileError
    {
        public static void Error(string message, bool exit)
        {
            Console.Error.WriteLine(message);
            if (exit)
            {
                Environment.Exit(1);
            }
        }

        public static void ErrorAt(string message, int line, int column, string stmt, bool exit)
        {
            // TODO: implement
        }
    }
}