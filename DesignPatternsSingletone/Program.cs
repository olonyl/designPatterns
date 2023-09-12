using DesignPatternsSingletone.BasicImplementation;
using System.Net.NetworkInformation;
using static System.Console;

namespace DesignPatternsSingletone
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var db = SingletonDatabase.Instance;
            var db2 = SingletonDatabase.Instance;

            var city = "Mexico City";

            WriteLine($"City {city} has population {db.GetPopulation(city)}");
        }
    }
}