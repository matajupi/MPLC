using System;

namespace mplc
{
    class X8664GenerateVisitor : AssemblyGenerateVisitor
    {
        private int IdentNumber = 0;

        public override void Initialize()
        {
            this.Add(".intel_syntax noprefix");
            this.Add(".globl main");
            // Debug ===============================================================
            this.Add("main:");

            // Prologue
            this.Add("    push rbp");
            this.Add("    mov rbp, rsp");
            // ここで変数分だけrspを引く(初期化をここでしてしまう)
            // asm.Add("    sub rsp, 16");
            // Debug ===============================================================
        }

        public override void Visit(ProgramNode node)
        {
            foreach (var n in node.StatementNodes) n.Accept(this);
        }

        public override void Visit(StatementNode node)
        {
            node.Node.Accept(this);
        }

        public override void Visit(ReturnNode node)
        {
            node.Node.Accept(this);
            this.Add("   pop rax");
            this.Add("   mov rsp, rbp");
            this.Add("   pop rbp");
            this.Add("   ret");
        }

        public override void Visit(IfNode node)
        {
            var pnum = this.IdentNumber++;
            node.ConditionNode.Accept(this);
            this.Add("   pop rax");
            this.Add("   cmp rax, 0");
            this.Add($"   je .Lelse{pnum}");
            node.StatementNode.Accept(this);
            this.Add($"   jmp .Lend{pnum}");
            this.Add($".Lelse{pnum}:");
            if (node.ElseNode != default)
                node.ElseNode.Accept(this);
            this.Add($".Lend{pnum}:");
        }

        public override void Visit(ElseNode node)
        {
            node.StatementNode.Accept(this);
        }

        public override void Visit(ExpressionNode node)
        {
            node.Node.Accept(this);
        }

        public override void Visit(AssignNode node)
        {
            if (node.LeftSide is LocalVariableNode && node.RightSide != default)
            {
                this.ComputeLocalVariableAddress(node.LeftSide as LocalVariableNode);
                node.RightSide.Accept(this);
                this.Add("   pop rdi");
                this.Add("   pop rax");
                this.Add("   mov [rax], rdi");
                this.Add("   push rdi");
                return;
            }
            node.LeftSide.Accept(this);
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
                        this.Add("   sete al");
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
            node.Node.Accept(this);
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
            this.ComputeLocalVariableAddress(node);
            this.Add("   pop rax");
            this.Add("   mov rax, [rax]");
            this.Add("   push rax");
        }

        public void BinaryOperation(Action operateAction)
        {
            this.Add("   pop rdi");
            this.Add("   pop rax");
            operateAction();
            this.Add("   push rax");
        }

        public void ComputeLocalVariableAddress(LocalVariableNode node)
        {
            var lvar = node.LocalVariable;
            this.Add("   mov rax, rbp");
            this.Add($"   sub rax, {lvar.Offset}");
            this.Add("   push rax");
        }
    }
}