using System;

namespace mplc
{
    class AssignNode : Node
    {
        public Node LeftSide { get; set; }
        public Node RightSide { get; set; }

        public override void Parse(Context context)
        {
            // TODO: 代入のコードを改善
            // 方法1: トークンの２つ先読みして判断（トークンのポインタは動かさず確認だけ）　有力候補
            // 方法2: TerminalExpressionまでさかのぼる（Interfaceを実装？ GetTerminalExpressions）　有力候補
            // 方法3: レジスタにアドレスを保管しておく（無駄なコードが残る）
            // 方法4: 生成されたコードの余分な部分を無理やり削除する（現在採用中）
            // 方法5: IdentifierとEqualを先読みしてEqualがない場合与えられたトークンより普通に処理を
            // 続けられるように下位のNodeに細工を施す（下位のNodeに影響するため意外と実装重い）
            this.LeftSide = new EqualityNode();
            this.LeftSide.Parse(context);
            if (context.Consume(Tokenizer.TokenKind.EQUAL))
            {
                this.RightSide = new EqualityNode();
                this.RightSide.Parse(context);
            }
        }

        public override void Accept(AssemblyGenerateVisitor v)
        {
            v.Visit(this);
        }
    }
}