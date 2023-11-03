using Coding.Exercise;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MementoExcercise.DesignPattern
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var tMachine = new TokenMachine();
            var token = new Token(111);

            var memento = tMachine.AddToken(token);
            token.Value = 333;

            var token2 = new Token(222);
            var memento3 = tMachine.AddToken(token2);

            var token3 = new Token(100);
            var memento4 = tMachine.AddToken(token3);

            tMachine.Revert(memento3);

            Console.WriteLine(tMachine.Tokens.Count);
        }
    }
}

namespace Coding.Exercise
{
    public class Token
    {
        public int Value = 0;

        public Token(int value)
        {
            Value = value;
        }
    }

    public class Memento
    {
        public List<Token> Tokens = new List<Token>();
    }

    public class TokenMachine
    {
        public List<Token> Tokens = new List<Token>();

        public Memento AddToken(int value)
        {
            return AddToken(new Token(value));
        }

        public Memento AddToken(Token token)
        {
            Tokens.Add(token);
            var m = new Memento();
            // a rather roundabout way of cloning
            m.Tokens = Tokens.Select(t => new Token(t.Value)).ToList();
            return m;
        }

        public void Revert(Memento m)
        {
            Tokens = m.Tokens.Select(mm => new Token(mm.Value)).ToList();
        }
    }
}
