namespace DesignPatternFactory.AbstractFactory
{
    public interface IHotDrinkFactory
    {
        IHotDrink Prepare(int amount);
    }
}

