using System;
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

        public BankAccount(int balance)
        {
            this.balance = balance;
        }
        public Memento Deposit(int amount)
        {
            balance += amount;
            return new Memento(balance);
        }

        public void Restore(Memento memento)
        {
            balance = memento.Balance;
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
            var m1 = ba.Deposit(50);
            var m2 = ba.Deposit(25);

            WriteLine(ba);

            ba.Restore(m1);
            WriteLine(ba);

            ba.Restore(m2);
            WriteLine(ba);
        }
    }
}
