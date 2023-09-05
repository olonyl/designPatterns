using DesignPatternsConsole.StepwiseBuilder;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using static System.Console;
using DesignPatternsConsole.Builder;
using System.Security.Policy;
using DesignPatternsConsole.FunctionalBuilderSimple;
using DesignPatternsConsole.FunctionalBuilderComplex;
using System;
using System.Runtime.InteropServices.WindowsRuntime;
using DesignPatternsConsole.FacadeBuilder;

namespace DesignPatternsConsole
{
    internal partial class Program
    {

        static void Main(string[] args)
        {
            // ExecuteHtmlBuilder();
            // FluentBuilderInheritanceWithRecursiveGenerics();
            // StepWiseBuilder();
            // FunctionalBuilderSimple();
            // FunctionalBuilderComplex();
            //FacadeBuilder();

            ReadLine();


        }

        private static void FacadeBuilder()
        {
            var pb = new FacadeBuilder.PersonBuilder();
            FacadeBuilder.Person person = pb
                .Works.At("Fab")
                    .AsA("Engineer")
                    .Earning(1000)
                .Lives.At("TP")
                     .WithPostcode("12345")
                     .In("Managua");
            Console.WriteLine(person);
        }

        private static void FunctionalBuilderComplex()
        {
            var person = new FunctionalBuilderComplex.PersonBuilder()
                .Called("Sarah")
                .WorksAs("Developer")
                .Build();
        }

        private static void FunctionalBuilderSimple()
        {
            var person = new FunctionalBuilderSimple.PersonBuilder()
                .Called("Sarah")
                .WorksAs("Developer")
                .Build();
        }

        private static void StepWiseBuilder()
        {
            var car = CarBuilder.Create()
                .OfType(CarType.Crossover)
                .WithWheels(18)
                .Build();
        }
        private static void FluentBuilderInheritanceWithRecursiveGenerics()
        {
            var me = DesignPatternsConsole.FluentBuilderInheritanceWithRecursiveGenerics.Person.New
                             .Called("Olonyl")
                             .WorksAs("Quant")
                             .Build();
            WriteLine(me);
        }



        public static void ExecuteHtmlBuilder()
        {
            var hello = "hello";
            var sb = new StringBuilder();
            sb.Append("<p>");
            sb.Append(hello);
            sb.Append("</p");


            var words = new[] { "hello", "world" };
            sb.Clear();
            sb.Append("<ul>");
            foreach (var word in words)
            {
                sb.AppendFormat("<li>{0}</li>", word);
            }
            sb.Append("</ul>");

            WriteLine(sb);

            var builder = new HtmlBuilder("ul");

            builder.AddChild("li", "hello")
                   .AddChild("li", "world");

            WriteLine(builder);
        }
    }
}
