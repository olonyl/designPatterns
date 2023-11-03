﻿using Autofac;
using ImpromptuInterface;
using System;
using System.Dynamic;
using System.Runtime.CompilerServices;
using static System.Console;

namespace NullObjectDesignPattern
{
    public interface ILog
    {
        void Info(string msg);
        void Warn(string msg);

        public static ILog Null = NullLog.Instance;

        private sealed class NullLog : ILog
        {
            private static Lazy<NullLog> instance = new Lazy<NullLog>((() => new NullLog()));
            public static ILog Instance => instance.Value;

            public void Info(string msg)
            {
                throw new NotImplementedException();
            }

            public void Warn(string msg)
            {
                throw new NotImplementedException();
            }
        }
    }

    class ConsoleLog : ILog
    {
        public void Info(string msg)
        {
           WriteLine(msg);
        }

        public void Warn(string msg)
        {
            WriteLine($"WARNING!! {msg}");
        }
    }

    public class NullLog : ILog
    {
        public void Info(string msg) { }

        public void Warn(string msg) { }
    }

    public class BankAccount
    {
        private ILog log;
        private int balance;

        public BankAccount(ILog log)
        {
            this.log = log;
        }

        public void Deposit(int amount)
        {
            balance += amount;
            log?.Info($"Deposited {amount}, balance is now {balance}");
        }
    }

    public class Null<TInterface> : DynamicObject where TInterface: class
    {
        public static TInterface Instance =>new Null<TInterface>().ActLike<TInterface>();

        public override bool TryInvokeMember(InvokeMemberBinder binder, object[] args, out object result)
        {
            result  = Activator.CreateInstance(binder.ReturnType);
            return true;
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            //  var log = new ConsoleLog();
            // var ba = new BankAccount(null);
            // ba.Deposit(100);

            //var cb = new ContainerBuilder();
            //cb.RegisterType<BankAccount>();
            //cb.RegisterType<NullLog>().As<ILog>();

            //using(var c = cb.Build()) { 
            //    var ba = c.Resolve<BankAccount>();
            //    ba.Deposit(200);
            //}

            var log = Null<ILog>.Instance;
            log.Info("Test");

            var ba = new BankAccount(log);
            ba.Deposit(300);

        }
    }
}
