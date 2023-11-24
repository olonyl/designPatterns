using System;
using System.Text;

namespace Coding.Exercise
{
    public abstract class ExpressionVisitor
    {
        public abstract void Visit(Value value);

        public abstract void Visit(AdditionExpression ae);

        public abstract void Visit(MultiplicationExpression me);
    }

    public abstract class Expression
    {
        public abstract void Accept(ExpressionVisitor ev);
    }

    public class Value : Expression
    {
        public readonly int TheValue;

        public Value(int value)
        {
            TheValue = value;
        }

        public override void Accept(ExpressionVisitor ev)
        {
            ev.Visit(this);
        }

        // todo
    }

    public class AdditionExpression : Expression
    {
        public readonly Expression LHS, RHS;

        public AdditionExpression(Expression lhs, Expression rhs)
        {
            LHS = lhs;
            RHS = rhs;
        }

        public override void Accept(ExpressionVisitor ev)
        {
            ev.Visit(this);
        }

        // todo
    }

    public class MultiplicationExpression : Expression
    {
        public readonly Expression LHS, RHS;

        public MultiplicationExpression(Expression lhs, Expression rhs)
        {
            LHS = lhs;
            RHS = rhs;
        }

        public override void Accept(ExpressionVisitor ev)
        {
            ev.Visit(this);
        }

        // todo
    }

    public class ExpressionPrinter : ExpressionVisitor
    {
        public StringBuilder sb = new StringBuilder();
        public override void Visit(Value value)
        {
            sb.Append(value.TheValue);
        }

        public override void Visit(AdditionExpression ae)
        {
            sb.Append("(");
            ae.LHS.Accept(this);
            sb.Append("+");
            ae.RHS.Accept(this);
            sb.Append(")");
        }

        public override void Visit(MultiplicationExpression me)
        {
            me.LHS.Accept(this);
            sb.Append("*");
            me.RHS.Accept(this);
        }

        public override string ToString()
        {
            return sb.ToString();
        }

        static void Main(string[] args)
        {
            var addition = new AdditionExpression(new Value(1), new Value(2));
            var multiplication = new MultiplicationExpression(new Value(2), new Value(2));
            var composed = new MultiplicationExpression(new AdditionExpression(new Value(2), new Value(3)), new Value(4));

            var expPrinter = new ExpressionPrinter();
            expPrinter.Visit(addition);
            Console.WriteLine(expPrinter);
            expPrinter = new ExpressionPrinter();
            expPrinter.Visit(multiplication);
            Console.WriteLine(expPrinter);

            expPrinter = new ExpressionPrinter();
            expPrinter.Visit(composed);
            Console.WriteLine(expPrinter);


        }
    }
}