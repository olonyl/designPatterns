//using DesignPatternsPrototypes.Serialization;
using DesignPatternsPrototypes.Inheritance;
using System;
using System.Diagnostics;
using static System.Console;

namespace DesignPatternsPrototypes
{
    public class Point : IPrototype<Point>
    {
        public int X, Y;

        public Point DeepCopy()
        {
            var p = new Point();
            p.X = X;
            p.Y = Y;
            return p;
        }
    }

    public class Line : IPrototype<Line>
    {
        public Point Start, End;

        public Line DeepCopy()
        {
            var l = new Line();
            l.Start = Start;
            l.End = End;

            return l;
        }
    }

    public interface IPrototype<T>
    {
        T DeepCopy();
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            //CloningExample();
            //InheritanceExample();
            // SerializationExample();

            //var l = new Line();
        }

        private static void SerializationExample()
        {
            //var john = new Serialization.Person(new[] { "john", "smith" },
            //    new Serialization.Address("London Road", 123));

            ////var jane = john.DeepCopy();
            //var jane = john.DeepCopyXml();
            //jane.Address.HouseNumber = 321;
            //jane.Names[0] = "Janes";
            //WriteLine(john);
            //WriteLine(jane);
        }

        private static void InheritanceExample()
        {
            var john = new Inheritance.Employee();
            john.Names = new[] { "John", "Doe" };
            john.Address = new Inheritance.Address("Maple Street", 123);
            john.Salary = 13322;

            var copy = john.DeepCopy();
            copy.Names[1] = "Landeros";
            copy.Address.HouseNumber++;
            copy.Salary = 9000;

            WriteLine(john);
            WriteLine(copy);
        }

        private static void CloningExample()
        {
            var john = new Cloning.Person(new[] { "John", "Smith" }, new Cloning.Address("London Road", 123));

            //var jane = new Person(john);
            var jane = (Cloning.Person)john.Clone();
            //var jane = john.DeepCopy();
            jane.Address.HouseNumber = 333;
            jane.Names[0] = "Jane";

            Console.WriteLine(john);
            Console.WriteLine(jane);
        }
    }
}