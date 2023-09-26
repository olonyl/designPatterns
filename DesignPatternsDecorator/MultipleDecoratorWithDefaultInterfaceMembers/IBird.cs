using System;

namespace DesignPatternsDecorator.MultipleDecoratorWithDefaultInterfaceMembers
{
    public interface IBird : ICreature
    {
        void Fly()
        {
            if (Age >= 10)
            {
                Console.WriteLine("I am flying");
            }
        }
    }
}
