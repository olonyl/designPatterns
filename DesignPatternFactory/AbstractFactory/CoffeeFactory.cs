using static System.Console;

namespace DesignPatternFactory.AbstractFactory
{
    internal class CoffeeFactory : IHotDrinkFactory
    {
        public IHotDrink Prepare(int amount)
        {
            WriteLine($"Grind some beans, boil water, pour {amount} ml, add cream and sugar, and enjoy!");
            return new Coffee();
        }
    }
}

