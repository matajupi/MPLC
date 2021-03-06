using System;
using System.Collections.Generic;

namespace mplc
{
    class Tokenizer
    {
        // すべてのオペレータとキーワードをEnumでラップ
        public enum TokenKind
        {
            TK_EQUAL,
            TK_RETURN,
            TK_NUM,
        }

        // TokenKindよりラップされた文字列に逆参照する
        public IReadOnlyDictionary<TokenKind, string> TokenString = new Dictionary<TokenKind, string>()
        {
            { TokenKind.TK_EQUAL, "=" },
            { TokenKind.TK_RETURN, "return"},
            { TokenKind.TK_NUM , "num" },
        };

        private readonly char[] TokenChars = new char[] { ' ', '\n', '\t', '\r', '\f' };

        public string Text { get; }
        public string[] Tokens { get; }
        public int Index { get; set; }

        public Tokenizer(string text)
        {
            this.Text = text;
            this.Tokenize();
            this.Index = 0;
        }
        
        private string[] Tokenize()
        {
            for (var p = 0; this.Text.Length > p; p++)
            {
            }

            var tokens = new List<string>();
            foreach (var s in seped)
            {
                if (s == "") continue;
                tokens.Add(s);
            }
            this.Tokens = tokens.ToArray();
            return this.Tokens;
        }

        public string NextToken()
        {
            var token = this.Tokens[this.Index];
            this.Index++;
            return token;
            // 返すときはPair<TokenKind, string>
        }

        public bool HasMoreToken() => this.Tokens.Length > this.Index;
    }
}
