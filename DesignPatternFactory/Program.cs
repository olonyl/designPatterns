using DesignPatternFactory.AbstractFactory;
using DesignPatternFactory.AsynchronousFactoryMethod;
using DesignPatternFactory.FactoryMethod;
using DesignPatternFactory.InnerFactory;
using DesignPatternFactory.ObjectTrackingAndBulkReplacement;
using System;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;
using static System.Console;

namespace DesignPatternFactory
{
    internal interface IHotDrink
    {
        void Consume();
    }

    internal class Tea : IHotDrink
    {
        public void Consume()
        {
            WriteLine($"This Tea is delicious");
        }
    }

    internal class Coffee : IHotDrink
    {
        public void Consume()
        {
            WriteLine("This is a marvelous Coffee");
        }
    }

    internal interface IHotDrinkFactory
    {
        IHotDrink Prepare(int amount);
    }

    internal class TeaFactory : IHotDrinkFactory
    {
        public IHotDrink Prepare(int amount)
        {
            WriteLine($"Put in a tea bag, boil water, pour {amount} ml, add lemon, sugar, and enjoy!");
            return new Tea();
        }
    }

    internal class CoffeeFactory : IHotDrinkFactory
    {
        public IHotDrink Prepare(int amount)
        {
            WriteLine($"Grind some beans, boil water, pour {amount} ml, NO SUGAR, and enjoy!");
            return new Tea();   
        }
    }

    internal class HotDrinkMachine
    {
        private List<Tuple<string, IHotDrinkFactory>> factories =
            new List<Tuple<string, IHotDrinkFactory>>();

        public HotDrinkMachine()
        {
            foreach (var t in typeof(HotDrinkMachine).Assembly.GetTypes())
            {
                if(typeof(IHotDrinkFactory).IsAssignableFrom(t) &&
                    !t.IsInterface)
                {
                    factories.Add(Tuple.Create(t.Name.Replace("Factory", string.Empty),
                        (IHotDrinkFactory)Activator.CreateInstance(t)));
                }
            }
        }

        public IHotDrink MakeDrink()
        {
            WriteLine("Available Drinks: ");
            for (int index = 0; index < factories.Count; index++)
            {
                var tuple = factories[index];
                WriteLine($"{index}: {tuple.Item1}");
            }
            while (true)
            {
                string s;
                if((s = Console.ReadLine()) != null
                    && int.TryParse(s, out int i)
                    && i >= 0
                    && i < factories.Count){
                    WriteLine("Specify amount:");
                    s = Console.ReadLine();
                    if(s != null
                        && int.TryParse(s, out int amount)
                        && amount > 0)
                    {
                        return factories[i].Item2.Prepare(amount);
                    }
                }
                WriteLine("Incorrect input, try again");
            }            
        }
    }

    internal class Program
    {

        static void Main(string[] args)
        {
            // var point = Point.NewPolarPoint(1.0, Math.PI/2);

            // var x = await Foo.CreateAsync();
            //CallObjectTrachingAndBulkReplacement();
            //var point = InnerFactory.Point.Factory.NewPolarPoint(1.0, Math.PI / 2);


            //var machine = new HotDrinkMachine();
            //var drink = machine.MakeDrink(HotDrinkMachine.AvailableDrink.Tea, 10);
            //drink.Consume();

            var machine = new HotDrinkMachine();
            var drink = machine.MakeDrink();


            // WriteLine(point);
        }

        private static void CallObjectTrachingAndBulkReplacement()
        {
            var factory = new TrackingThemeFactory();
            var theme1 = factory.CreateTheme(false);
            var theme2 = factory.CreateTheme(true);
            WriteLine(factory.Info);

            var factory2 = new ReplaceableThemeFactory();
            var magicTheme = factory2.CreateTheme(true);
            WriteLine(magicTheme.Value.BgrColor);
            factory2.ReplaceTheme(false);
            WriteLine(magicTheme.Value.BgrColor);
        }
    }
}

