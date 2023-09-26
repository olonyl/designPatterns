using System;

namespace DesignPatternsDecorator.MultipleDecoratorWithDefaultInterfaceMembers
{
    public interface ILizar : ICreature
    {
        void Crawl()
        {
            if (Age < 10)
            {
                Console.WriteLine("I am crawling");
            }
        }
    }
}
