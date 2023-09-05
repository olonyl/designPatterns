using static System.Console;

namespace DesignPatternFactory.AbstractFactory
{
    internal class Tea : IHotDrink
    {
        public void Consume()
        {
            WriteLine("This tea is nice but I prefer it with milk.");
        }
    }
}

