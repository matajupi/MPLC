using System;
using System.Collections.Generic;

namespace mplc
{
    class BlockNode : Node
    {
        public List<Node> Nodes { get; set; }

        public BlockNode()
        => this.Nodes = new List<Node>();

        public override void Parse(Context context)
        {
            context.Expect(Tokenizer.TokenKind.LEFT_CURLY_BRACE);
            while (!context.Consume(Tokenizer.TokenKind.RIGHT_CURLY_BRACE))
            {
                var stmt = new StatementNode();
                stmt.Parse(context);
                this.Nodes.Add(stmt);
            }
        }

        public override void Accept(AssemblyGenerateVisitor v)
        {
            v.Visit(this);
        }
    }
}