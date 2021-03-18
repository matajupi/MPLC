using System;

namespace mplc
{
    class X8664GenerateVisitor : AssemblyGenerateVisitor
    {
        public override void Initialize()
        {
            this.Add(".intel_syntax noprefix");
            this.Add(".globl main");
        }

        public override void Visit(ProgramNode node)
        {
            foreach (var n in node.StatementNodes) n.Accept(this);
        }

        public override void Visit(StatementNode node)
        {
            node.ExpressionNode.Accept(this);
        }

        public override void Visit(ExpressionNode node)
        {
            node.AssignNode.Accept(this);
        }

        public override void Visit(AssignNode node)
        {
            node.LeftSide.Accept(this);
            // TODO: Implement <equality> ("=" <equality>)?
        }

        public override void Visit(EqualityNode node)
        {
            node.LeftSide.Accept(this);
            if (node.RightSide != default && node.Operator != default)
            {
                node.RightSide.Accept(this);

                this.BinaryOperation(() => 
                {
                    this.Add("   cmp rax, rdi");
                    switch (node.Operator)
                    {
                        case Tokenizer.TokenKind.EQUAL_EQUAL:
                        this.Add("   seteal");
                        break;
                        case Tokenizer.TokenKind.EXCLAMATION_EQUAL:
                        this.Add("   setne al");
                        break;
                    }
                    this.Add("   movzb rax, al");
                });
            }
        }

        public override void Visit(RelationalNode node)
        {
            node.LeftSide.Accept(this);
            if (node.RightSide != default && node.Operator != default)
            {
                node.RightSide.Accept(this);

                this.BinaryOperation(() => 
                {
                    this.Add("   cmp rax, rdi");
                    switch (node.Operator)
                    {
                        case Tokenizer.TokenKind.LESS:
                        this.Add("   setl al");
                        break;
                        case Tokenizer.TokenKind.LESS_EQUAL:
                        this.Add("   setle al");
                        break;
                    }
                    this.Add("   movzb rax, al");
                });
            }
        }

        public override void Visit(AdditionNode node)
        {
            node.LeftSide.Accept(this);

            foreach (var pair in node.RightSides)
            {
                pair.Item2.Accept(this);

                this.BinaryOperation(() =>
                {
                    switch (pair.Item1)
                    {
                        case Tokenizer.TokenKind.PLUS:
                        this.Add("   add rax, rdi");
                        break;
                        case Tokenizer.TokenKind.MINUS:
                        this.Add("   sub rax, rdi");
                        break;
                    }
                });
            }
        }

        public override void Visit(MultiplicationNode node)
        {
            node.LeftSide.Accept(this);

            foreach (var pair in node.RightSides)
            {
                pair.Item2.Accept(this);

                this.BinaryOperation(() =>
                {
                    switch (pair.Item1)
                    {
                        case Tokenizer.TokenKind.ASTERISK:
                        this.Add("   imul rax, rdi");
                        break;
                        case Tokenizer.TokenKind.SLASH:
                        this.Add("   cqo");
                        this.Add("   idiv rdi");
                        break;
                    }
                });
            }
        }

        public override void Visit(UnaryNode node)
        {
            node.PrimaryNode.Accept(this);
        }

        public override void Visit(PrimaryNode node)
        {
            node.Node.Accept(this);
        }

        public override void Visit(NumericLiteralNode node)
        {
            this.Add($"   push {node.Number}");
        }

        public override void Visit(LocalVariableNode node)
        {
            throw new NotImplementedException();
        }

        public void BinaryOperation(Action operateAction)
        {
            this.Add("   pop rdi");
            this.Add("   pop rax");
            operateAction();
            this.Add("   push rax");
        }
    }
}