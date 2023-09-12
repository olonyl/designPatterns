using DesignPatternsSingletone.BasicImplementation;
using System;
using System.Collections.Generic;

namespace DesignPatternsSingletone.DependencyInjection
{
    public class ConfigurableRecordFinder
    {
        private IDatabase database;

        public ConfigurableRecordFinder(IDatabase database)
        {
            this.database = database ?? throw new ArgumentNullException(nameof(database));
        }
        public int GetTotalPopulation(IEnumerable<string> names)
        {
            int result = 0;
            foreach (string name in names)
                result += database.GetPopulation(name);
            return result;
        }
    }
}
