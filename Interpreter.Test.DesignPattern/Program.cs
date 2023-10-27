using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using Interpreter.Test.DesignPattern;

namespace Interpreter.Test.DesignPattern
{

    public interface IElement
    {
        int Value { get;  }
    }

    public class Integer : IElement
    {
        public int Value { get; }

        public Integer(int value)
        {
            Value = value;
        }
    }

    public class Token
    {
        public enum Type
        {
            Integer,
            Addition,
            Subtraction,
            Variable
        }

        public Type MyType;
        public string Text;

        public override string ToString()
        {
            return $"`{Text}`";
        }
    }

    public class BinaryOperations 
        : IElement
    
    {
        public enum Type
        {
            Addition, Subtraction
        }

        public Type MyType;
        public IElement Left;
        public IElement Right;

        public int Value
        {
            get
            {
                var lValue = Left?.Value ?? 0;
                var rValue = Right?.Value ?? 0;

                switch (MyType)
                {
                    case Type.Addition:
                        return lValue + rValue;
                        break;
                    case Type.Subtraction:
                        return lValue + rValue;
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }
    }

    public class Lexer : List<Token>
    {
        public Lexer(string expression)
        {
            for (var index = 0; index < expression.Length; index++)
            {
                var value = expression[index];
                if (value == '+')
                    this.Add(new Token { MyType = Token.Type.Addition, Text = value.ToString() });
                else if (value == '-')
                    this.Add(new Token { MyType = Token.Type.Subtraction, Text = value.ToString() });
                else if (char.IsDigit(value))
                {
                    var sb = new StringBuilder(value.ToString());
                    if (index + 1 < expression.Length)
                    {
                        for (var j = index + 1; j < expression.Length; j++)
                        {
                            var innerValue = expression[j];
                            if (char.IsDigit(innerValue))
                            {
                                sb.Append(innerValue);
                                index++;
                            }
                            else
                            {
                                this.Add(new Token { MyType = Token.Type.Integer, Text = sb.ToString() });
                                sb.Clear();
                                break;
                            }
                        }
                        if (sb.Length > 0)
                            this.Add(new Token { MyType = Token.Type.Integer, Text = sb.ToString() });
                    }
                    else{
                        this.Add(new Token { MyType = Token.Type.Integer, Text = sb.ToString() });
                    }
                }else if (char.IsLetter(value))
                {
                    var sb = new StringBuilder(value.ToString());
                    if (index + 1 < expression.Length)
                    {
                        for (var j = index + 1; j < expression.Length; j++)
                        {
                            var innerValue = expression[j];
                            if (char.IsLetter(innerValue))
                            {
                                sb.Append(innerValue);
                                index++;
                            }
                            else
                            {
                                this.Add(new Token { MyType = Token.Type.Variable, Text = sb.ToString() });
                                break;
                            }
                        }
                        if (sb.Length > 0)
                            this.Add(new Token { MyType = Token.Type.Variable, Text = sb.ToString() });
                    }
                    else
                    {
                        this.Add(new Token { MyType = Token.Type.Variable, Text = sb.ToString() });
                    }
                }
                else throw new InvalidOperationException();
            }
        }
    }

    public class MyInterpreter : IElement
    {
        private readonly Dictionary<char, int> variables ;
        private readonly IReadOnlyList<Token> tokens;

        public MyInterpreter(Dictionary<char, int> variables, IReadOnlyList<Token> tokens)
        {
            this.variables = variables;
            this.tokens = tokens;
        }

        private IElement Parse(int initialIndex = 0, IElement leftElement = null,IReadOnlyList<Token> expression = null)
        {
            var operation = new BinaryOperations();
            var hasleft = false;

            for (var index = 0; index < expression.Count; index++)
            {
                var token = expression[index];
                if (leftElement != null && operation.Left == null)
                {
                    operation.Left = leftElement;
                    hasleft = true;
                }
                
                if (token.MyType == Token.Type.Addition)
                {
                    operation.MyType = BinaryOperations.Type.Addition;
                }
                else if (token.MyType == Token.Type.Subtraction)
                {
                    operation.MyType = BinaryOperations.Type.Subtraction;
                }
                else if (token.MyType == Token.Type.Variable || token.MyType == Token.Type.Integer)
                {
                    var key = token.Text[0];
                    int value;

                    if (token.MyType == Token.Type.Variable)
                        variables.TryGetValue(key, out value);
                    else
                        value = int.Parse(token.Text);

                    var integer = new Integer(value);
                    if (!hasleft)
                    {
                        operation.Left = integer;
                        hasleft = true;
                    }
                    else
                    {
                        operation.Right = integer;
                        if(index < expression.Count - 1)
                            return Parse(index++, operation, expression.Skip(index).Take(expression.Count - index).ToList()); 
                    }
                }
                else
                {
                    throw new ArgumentOutOfRangeException();
                }
            }

            return operation;
        }

        public int Value => tokens.Count(t=> t.MyType == Token.Type.Variable && t.Text.Length>1) > 0 ? 0: Parse(expression: tokens).Value;
    }
}

public class ExpressionProcessor
{
    public Dictionary<char, int> Variables = new Dictionary<char, int>  {{'x',3}};

    public int Calculate(string expression)
    {
        var lexer = new Lexer(expression);
        var interpreter = new MyInterpreter(Variables, lexer);

        return interpreter.Value;
    }
}



internal class Program
{
    static void Main(string[] args)
    {
        var result = new ExpressionProcessor().Calculate("1+2+");

        Console.WriteLine(result);
    }
}
