using System;

namespace Observer.KeywordEvent
{
    public class FallsIllEventArgs
    {
        public string Address;
    }
    public class Person
    {
        public void CatchACold()
        {
            FallsIll?.Invoke(this, new FallsIllEventArgs { Address="Barrio Tierra Prometida"});
        }

        public event EventHandler<FallsIllEventArgs> FallsIll;

    }
    internal class Program
    {
        static void Main(string[] args)
        {
            var person = new Person();

            person.FallsIll += CallDoctor;
            person.CatchACold();
            person.FallsIll -= CallDoctor;
        }

        private static void CallDoctor(object sender, FallsIllEventArgs e)
        {
            Console.WriteLine($"A doctor has been called to {e.Address}");
        }
    }
}
