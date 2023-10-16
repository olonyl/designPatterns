using System;

namespace ProxyDesignPattern.ProtectionProxy
{
    public class Car : ICar
    {
        public void Drive()
        {
            Console.WriteLine("Car is being driven");
        }
    }
}