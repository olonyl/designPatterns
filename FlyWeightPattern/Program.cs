using JetBrains.dotMemoryUnit;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FlyWeightPattern
{
    public class User
    {
        private string fullName;

        public User(string fullName)
        {
            this.fullName = fullName;   
        }
    }

    [TestFixture]
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
        }
        [Test] public void TestUser()
        {
            var firstNames = Enumerable.Range(0, 100).Select(_ => RandomString());
            var lastNames = Enumerable.Range(0, 100).Select(_ => RandomString());

            var users = new List<User>();
            foreach (var firstName in firstNames)
                foreach(var lastName in lastNames)
                    users.Add(new User($"{firstName} {lastName}"));

            ForceGC();

            dotMemory.Check(memory => Console.WriteLine(memory.SizeInBytes));
        }
        private string RandomString()
        {
            Random rand = new Random();
            return new string(
                Enumerable.Range(0, 10)
                .Select(_ => (char)('a' + rand.Next(26)))
                .ToArray());
        }
        private void ForceGC()
        {
            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();
        }
    }
}
