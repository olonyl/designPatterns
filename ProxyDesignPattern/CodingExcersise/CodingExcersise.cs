
namespace ProxyDesignPattern.CodingExcersise
{
    public interface IPerson
    {
        string Drink();
        string Drive();
        string DrinkAndDrive();
    }

    public class Person : IPerson
    {
        public int Age { get; set; }

        public string Drink()
        {
            return "drinking";
        }

        public string Drive()
        {
            return "driving";
        }

        public string DrinkAndDrive()
        {
            return "driving while drunk";
        }
    }

    public class ResponsiblePerson : IPerson
    {
        private readonly Person person;

        public ResponsiblePerson(Person person)
        {
            this.person = person;
        }

        public string Drink()
        {
            return Age < 18 ? "too young" : person.Drink();
        }

        public string Drive()
        {
            return Age < 16 ? "too young" : person.Drive();
        }

        public string DrinkAndDrive()
        {
            return "dead";
        }

        public int Age => person.Age;
    }
}