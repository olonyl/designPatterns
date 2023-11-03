using System;
using System.Collections.Generic;
using System.Threading;
using static System.Console;

namespace Memento.DesignPattern
{
    public class Memento
    {
        public int Balance { get; }
        public Memento(int balance)
        {
            Balance = balance;
        }
    }
    public class BankAccount
    {
        int balance;
        private List<Memento> changes = new List<Memento>();
        private int current;
        public BankAccount(int balance)
        {
            this.balance = balance;
            changes.Add(new Memento(balance));
        }
        public Memento Deposit(int amount)
        {
            balance += amount;
            var m = new Memento(balance);
            changes.Add(m);
            current++;

            return m;
        }

        public Memento Restore(Memento memento)
        {
            if( memento== null )
            {
                balance = memento.Balance;
                changes.Add(memento);
                return memento;
            }
            return null;
        }

        public Memento Undo()
        {
            if(current > 0)
            {
                var m = changes[--current];
                balance = m.Balance;
                return m;
            }
            return null;
        }

        public Memento Redo()
        {
            if (current + 1 < changes.Count)
            {
                var m = changes[++current];
                balance = m.Balance;
                return m;
            }
            return null;
        }

        public override string ToString()
        {
            return $"{nameof(balance)} : {balance}";
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            var ba = new BankAccount(100);
            ba.Deposit(50);
            ba.Deposit(25);
            WriteLine(ba);

            ba.Undo();
            WriteLine($"Undo 1: {ba}");

            ba.Undo();
            WriteLine($"Undo 2: {ba}");

            ba.Redo();
            WriteLine($"Redo 1: {ba}");

            ba.Deposit(80);
            WriteLine(ba);

            ba.Undo();
            WriteLine($"Undo 3: {ba}");

            ba.Redo();
            WriteLine($"Undo 2: {ba}");

            ba.Undo();
            WriteLine($"Redo 3: {ba}");

            ba.Deposit(200);
            WriteLine(ba);

        }
    }
}
