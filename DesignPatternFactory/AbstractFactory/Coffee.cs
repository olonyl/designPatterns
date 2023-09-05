using static System.Console;

namespace DesignPatternFactory.AbstractFactory
{
    internal class Coffee : IHotDrink
    {
        public void Consume()
        {
            WriteLine("This coffee is sensational!");
        }
    }
}

