using System;
using System.Text;

namespace IntrusiveVisitor
{
    public abstract class Expression
    {
        public abstract void Print(StringBuilder sb);
    }

    public class DoubleExpression : Expression
    {
        private double value;
        public DoubleExpression(double value)
        {
            this.value = value;
        }

        public override void Print(StringBuilder sb)
        {
            sb.Append(value);
        }
    }

    public class AdditionExprssion : Expression
    {
        private Expression left, right;

        public AdditionExprssion(Expression left, Expression right)
        {
            this.left = left;
            this.right = right;
        }

        public override void Print(StringBuilder sb)
        {
            sb.Append("(");
            left.Print(sb);
            sb.Append("+");
            right.Print(sb);
            sb.Append(")"); 
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            var e = new AdditionExprssion(
                new DoubleExpression(1),
                new AdditionExprssion(new DoubleExpression(2), 
                                      new DoubleExpression(3)));

            var sb = new StringBuilder();
            e.Print(sb);

            Console.WriteLine(sb.ToString());
        }
    }
}
