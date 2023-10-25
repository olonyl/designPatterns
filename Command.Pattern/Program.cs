﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using static System.Console;

namespace Command.Pattern
{
    public class BankAccount
    {
        public int balance;
        public int overdraftLimit = -500;

        public void Deposit(int amount)
        {
            balance += amount;
            WriteLine($"Deposited ${amount}, balance is now {balance}");
        }

        public bool Withdraw(int amount)
        {
            if (balance - amount >= overdraftLimit)
            {
                balance -= amount;
                WriteLine($"Withdrew ${amount}, balance is now {balance}");
                return true;
            }
            return false;
        }

        public override string ToString()
        {
            return $"{nameof(balance)}: {balance}";
        }
    }

    public interface ICommand
    {
        void Call();
        void Undo();
        bool Success { get; set; }

    }

    public class BankAccountCommand : ICommand
    {
        private BankAccount account;

        public enum Action
        {
            Deposit, Withdraw
        }

        private Action action;
        private int amount;

        public BankAccountCommand(BankAccount account, Action action, int amount)
        {
            this.account = account;
            this.action = action;
            this.amount = amount;
        }

        public void Call()
        {
            switch (action)
            {
                case Action.Deposit:
                    account.Deposit(amount);
                    Success = true;
                    break;
                case Action.Withdraw:
                    Success = account.Withdraw(amount);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public void Undo()
        {
            if(!Success) return;
            switch (action)
            {
                case Action.Deposit:
                    account.Withdraw(amount);
                    break;
                case Action.Withdraw:
                    account.Deposit(amount);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public bool Success { get; set; }
    }


    public class CompositeBankAccountCommand
        : List<BankAccountCommand>, ICommand
    {
        public CompositeBankAccountCommand()
        {
            
        }

        public CompositeBankAccountCommand(IEnumerable<BankAccountCommand> collection)
            : base(collection)
        {
            
        }
        public virtual void Call() => ForEach(cmd => cmd.Call());

        public virtual void Undo() => ForEach(cmd => cmd.Undo());

        public virtual bool Success
        {
            get { return this.All(cmd => cmd.Success); }
            set
            {
                foreach (var cmd in this)
                    cmd.Success = value;
            }
        }
    }

    public class MoneyTransferCommand : CompositeBankAccountCommand
    {
        public MoneyTransferCommand(BankAccount from 
            , BankAccount to
            , int amount)
        {
            AddRange(new[]
            {
                new BankAccountCommand(from, BankAccountCommand.Action.Withdraw, amount),
                new BankAccountCommand(to, BankAccountCommand.Action.Deposit, amount)
            });
        }

        public override void Call()
        {
            BankAccountCommand last = null;
            foreach (var cmd in this)
            {
                if (last == null || last.Success)
                {
                    cmd.Call();
                    last = cmd;
                }
                else
                {
                    cmd.Undo();
                    break;
                }
            }
        }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            //PerformSimpleOperations();

            var from = new BankAccount();
            from.Deposit(100);

            var to = new BankAccount();

            var mtc = new MoneyTransferCommand(from, to, 1000);
            mtc.Call();

            WriteLine(from);
            WriteLine(to);

            mtc.Undo();
            WriteLine(from);
            WriteLine(to);

        }

        public static void PerformSimpleOperations()
        {
            var ba = new BankAccount();
            var deposit = new BankAccountCommand(ba,
                BankAccountCommand.Action.Deposit, 100);
            var withdraw = new BankAccountCommand(ba,
                BankAccountCommand.Action.Withdraw, 50);
            var composite = new CompositeBankAccountCommand(
                new[] { deposit, withdraw });

            composite.Call();
            WriteLine(ba);

            composite.Undo();
            WriteLine(ba);
        }
    }
}
