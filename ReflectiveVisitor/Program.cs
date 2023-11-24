using System;
using System.Text;
using System.Collections.Generic;

namespace ReflectiveVisitor
{
    using DicType = Dictionary<Type, Action<Expression, StringBuilder>>;
    public abstract class Expression
    {
    }

    public class DoubleExpression : Expression
    {
        public double Value;
        public DoubleExpression(double value)
        {
            this.Value = value;
        }
    }

    public class AdditionExprssion : Expression
    {
        public Expression Left, Right;

        public AdditionExprssion(Expression left, Expression right)
        {
            this.Left = left;
            this.Right = right;
        }

    }

    public static class ExpressionPrinter
    {
        public static void Print(Expression e, StringBuilder sb)
        {
            if(e is DoubleExpression de)
            {
                sb.Append(de.Value);
            }else if(e is AdditionExprssion ae)
            {
                sb.Append("(");
                Print(ae.Left, sb);
                sb.Append("+");
                Print(ae.Right,sb);
                sb.Append(")");
            }
        }

        public static void PrintAction(Expression e, StringBuilder sb)
        {
            actions[e.GetType()](e, sb);
        }

        private static DicType actions = new DicType
        {
            [typeof(DoubleExpression)] = (e, sb) =>
            {
                var de = (DoubleExpression)e;
                sb.Append(de.Value);
            },
            [typeof(AdditionExprssion)] = (e, sb) =>
            {
                var ae = (AdditionExprssion)e;
                sb.Append("(");
                Print(ae.Left, sb);
                sb.Append("+");
                Print(ae.Right, sb);
                sb.Append(")");

            }
        };

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

            ExpressionPrinter.Print(e, sb );

            Console.WriteLine(sb.ToString());

            var it = GetSomething();
        }

        public static IEnumerable<string> GetSomething()
        {
            var elem = new List<string>();
            foreach(var it in elem)
            {
                yield return it;
            }
        }
    }
}
