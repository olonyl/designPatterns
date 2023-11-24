using System;
using System.Text;

namespace DynamicVisitorWithDLR
{
    public abstract class Expression
    {

    }
    public class DoubleExpression: Expression
    {
        public double Value;
        public DoubleExpression(double value)
        {
            Value = value;
        }
    }

    public class AdditionExpression : Expression
    {
        public Expression Left, Right;

        public AdditionExpression(Expression left, Expression right)
        {
            Left = left;
            Right = right;
        }
    }

    public class ExpressionPrinter
    {
        public void Print(AdditionExpression ae, StringBuilder sb)
        {
            sb.Append("(");
            Print((dynamic)ae.Left, sb);
            sb.Append('+');
            Print((dynamic)ae.Right, sb);
            sb.Append(")");
        }
        public void Print(DoubleExpression de, StringBuilder sb)
        {
            sb.Append(de.Value);
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            var e = new AdditionExpression(new DoubleExpression(1),
                 new AdditionExpression(new DoubleExpression(2), new DoubleExpression(3)));
            var ep = new ExpressionPrinter();
            var sb = new StringBuilder();

            ep.Print(e, sb);
        }
    }
}
