using System;

namespace DesignPatternsDecorator.MultipleInheritanceWithInterfaces
{
    public class Bird : IBird
    {
        public int Weight { get; set; }
        public void Fly()
        {
            Console.WriteLine($"Soaring in the sky with weight: {Weight}");
        }
    }
}
