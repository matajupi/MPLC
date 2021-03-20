using System;
using System.Collections.Generic;

namespace mplc
{
    abstract class AssemblyGenerateVisitor
    {
        public List<string> Code { get; set; } = new List<string>();

        /// <summary>
        /// Initialize assembly code.
        /// </summary>
        public abstract void Initialize();
        public abstract void Visit(ProgramNode node);
        public abstract void Visit(StatementNode node);
        public abstract void Visit(ReturnNode node);
        public abstract void Visit(IfNode node);
        public abstract void Visit(ElseNode node);
        public abstract void Visit(WhileNode node);
        public abstract void Visit(ForNode node);
        public abstract void Visit(ExpressionNode node);
        public abstract void Visit(AssignNode node);
        public abstract void Visit(EqualityNode node);
        public abstract void Visit(RelationalNode node);
        public abstract void Visit(AdditionNode node);
        public abstract void Visit(MultiplicationNode node);
        public abstract void Visit(UnaryNode node);
        public abstract void Visit(PrimaryNode node);
        public abstract void Visit(NumericLiteralNode node);
        public abstract void Visit(LocalVariableNode node);
        
        public void Add(string line) => this.Code.Add(line);
    }
}