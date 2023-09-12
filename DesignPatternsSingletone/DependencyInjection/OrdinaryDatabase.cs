using DesignPatternsSingletone.BasicImplementation;
using MoreLinq;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using static System.Console;

namespace DesignPatternsSingletone.DependencyInjection
{
    public class OrdinaryDatabase : IDatabase
    {
        private Dictionary<string, int> capitals;

        private OrdinaryDatabase()
        {
            WriteLine("Initializing database");

            capitals = File.ReadAllLines(Path.Combine(
                    new FileInfo(typeof(IDatabase).Assembly.Location).DirectoryName, "capitals.txt"
                ))
                .Batch(2)
                .ToDictionary(list => list.ElementAt(0).Trim(),
                              list => int.Parse(list.ElementAt(1))
                 );
        }
        public int GetPopulation(string name)
        {
            return capitals[name];
        }
    }
}
