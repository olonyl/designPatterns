
using Autofac;
using System;
using System.ComponentModel;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;
using System.Threading;

namespace DesignPatternsDecorator
{
    public sealed class Money
    {
        public static readonly Money None = new Money(0, 0, 0, 0, 0, 0);
        public static readonly Money Cent = new Money(1, 0, 0, 0, 0, 0);
        public static readonly Money TenCent = new Money(0, 1, 0, 0, 0, 0);
        public static readonly Money Quarter = new Money(0, 0, 1, 0, 0, 0);
        public static readonly Money Dollar = new Money(0, 0, 0, 1, 0, 0);
        public static readonly Money FiveDollar = new Money(0, 0, 0, 0, 1, 0);
        public static readonly Money TwentyDollar = new Money(0, 0, 0, 0, 0, 1);

        public int OneCentCount { get; }
        public int TenCentCount { get; }
        public int QuarterCount { get; }
        public int OneDollarCount { get; }
        public int FiveDollarCount { get; }
        public int TwentyDollarCount { get; }

        public Money(
            int oneCentCount,
            int tenCentCount,
            int quarterCentCount,
            int oneDollarCount,
            int fiveDollarCount,
            int twentyDollarCount) : this()
        {
            OneCentCount = oneCentCount;
            TenCentCount = tenCentCount;
            QuarterCount = quarterCentCount;
            OneDollarCount = oneDollarCount;
            FiveDollarCount = fiveDollarCount;
            TwentyDollarCount = twentyDollarCount;
        }
        public Money()
        {

        }
        public static Money operator +(Money money1, Money money2)
        {
            Money sum = new Money(money1.OneCentCount + money2.OneCentCount,
                                    money1.TenCentCount + money2.TenCentCount,
                                    money1.QuarterCount + money2.QuarterCount,
                                    money1.OneDollarCount + money2.OneDollarCount,
                                    money1.FiveDollarCount + money2.FiveDollarCount,
                                    money1.TwentyDollarCount + money2.TwentyDollarCount);
            return sum;
        }

        public static Money operator -(Money money1, Money money2)
        {
            return new Money(money1.OneCentCount - money2.OneCentCount,
                                    money1.TenCentCount - money2.TenCentCount,
                                    money1.QuarterCount - money2.QuarterCount,
                                    money1.OneDollarCount - money2.OneDollarCount,
                                    money1.FiveDollarCount - money2.FiveDollarCount,
                                    money1.TwentyDollarCount - money2.TwentyDollarCount);
        }

        public static Money operator *(Money money1, int multiplier)
        {
            return new Money(money1.OneCentCount * multiplier,
                                    money1.TenCentCount * multiplier,
                                    money1.QuarterCount * multiplier,
                                    money1.OneDollarCount * multiplier,
                                    money1.FiveDollarCount * multiplier,
                                    money1.TwentyDollarCount * multiplier);
        }
        public override string ToString()
        {
            if (Amount < 1)
                return $"¢{(Amount * 100).ToString("0")}";

            return $"${Amount.ToString("0.00")}";
        }
        public decimal Amount
        {
            get => OneCentCount * 0.01m +
                    TenCentCount * 0.10m +
                    QuarterCount * 0.25m +
                    OneDollarCount +
                    FiveDollarCount * 5 +
                    TwentyDollarCount * 20;
        }
    }

    public class Temperature
    {
        public double Celsius { get; }

        public Temperature(double celsius)
        {
            Celsius = celsius;
        }

        public static explicit operator Fahrenheit(Temperature temperature)
        {
            double fahrenheit = temperature.Celsius * 9 / 5 + 32;
            return new Fahrenheit(fahrenheit);
        }
    }

    public class Fahrenheit
    {
        double value;
        public Fahrenheit(double value)
        {
            this.value = value;
        }
    
        public override string ToString()
        {
            return $"Fahrenheit: {value}";
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            Temperature celsius = new Temperature(25);
            Fahrenheit fahrenheit = (Fahrenheit) celsius; // implicit conversion from Celsius to Fahrenheit
            Console.WriteLine(fahrenheit); // output: 77
            Console.ReadLine();
            //AutoFacExample();

            //StaticDecoratorComposition();

            // MultipleInheritanceWithInterfaces();

            //MultipleDecoratorWithDefaultInterfaceMembers();

            //DynamicDecoratorCompositions();

            //ApplyDecoratorPolicy();

        }

        private static void AutoFacExample()
        {
            var b = new ContainerBuilder();

            b.RegisterType<ReportingService>().Named<IReportingService>("reporting");
            b.RegisterDecorator<IReportingService>(
                (context, service) => new ReportingServiceWithLogging(service), "reporting");

            using (var c = b.Build())
            {
                var r = c.Resolve<IReportingService>();
                r.Report();
            }
        }

        private static void StaticDecoratorComposition()
        {
            var redSquare = new StaticDecoratorComposition.ColoredShape<StaticDecoratorComposition.Square>("red");
            Console.WriteLine(redSquare.AsString());

            var squareTransparent = new
                StaticDecoratorComposition.TransparentShpae<
                    StaticDecoratorComposition.ColoredShape<StaticDecoratorComposition.Square>>(0.4f);
        }

        private static void ApplyDecoratorPolicy()
        {
            var square = new DesignPatternsDecorator.DetectingDecoratorCycles.Square(2);

            var colored1 = new DesignPatternsDecorator.DetectingDecoratorCycles.ColoredShape(square, "Red");
            var colored2 = new DesignPatternsDecorator.DetectingDecoratorCycles.ColoredShape(colored1, "Green");

            Console.WriteLine(colored2.AsString());
        }

        private static void DynamicDecoratorCompositions()
        {
            var square = new DynamicDecoratorComposition.Square(1.23f);
            Console.WriteLine(square.AsString());

            var redSquare = new DynamicDecoratorComposition.ColoredShape(square, "Red");
            Console.WriteLine(redSquare.AsString());

            var transparentSquare = new DynamicDecoratorComposition.TransparetShape(0.5f, redSquare);
            Console.WriteLine(transparentSquare.AsString());
        }

        private static void MultipleDecoratorWithDefaultInterfaceMembers()
        {
            var dragon = new MultipleDecoratorWithDefaultInterfaceMembers.Dragon { Age = 5 };

            if (dragon is MultipleDecoratorWithDefaultInterfaceMembers.IBird bird)
                bird.Fly();
            if (dragon is MultipleDecoratorWithDefaultInterfaceMembers.ILizar lizar)
                lizar.Crawl();

            Console.WriteLine("Hello World!");
        }

        private static void MultipleInheritanceWithInterfaces()
        {
            var d = new MultipleInheritanceWithInterfaces.Dragon();
            d.Weight = 123;
            d.Fly();
            d.Crawl();
        }
    }
}
