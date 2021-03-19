using System;
using System.Collections.Generic;

namespace mplc
{
    class ProgramNode : Node
    {
        public List<Node> StatementNodes { get; set; }

        public ProgramNode()
        => this.StatementNodes = new List<Node>();

        public override void Parse(Context context)
        {
            while (!context.AtEOF())
            {
                var node = new StatementNode();
                node.Parse(context);
                this.StatementNodes.Add(node);
            }
        }

        public override void Accept(AssemblyGenerateVisitor v)
        {
            v.Visit(this);
        }
    }
}