using System.Collections.Generic;

namespace DesignPatternsSingletone.BasicImplementation
{
    public class SingletonRecordFinder
    {
        public int GetTotalPopulation(IEnumerable<string> names)
        {
            int result = 0;
            foreach (string name in names)
                result += SingletonDatabase.Instance.GetPopulation(name);
            return result;
        }
    }
}
