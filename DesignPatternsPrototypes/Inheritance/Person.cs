namespace DesignPatternsPrototypes.Inheritance
{
    public class Person : IDeepCopyable<Person>
    {
        public string[] Names;
        public Address Address;
        public Person()
        {

        }
        public Person(string[] names, Address address)
        {
            Names = names;
            Address = address;
        }

        public void CopyTo(Person target)
        {
            target.Names = (string[]) Names.Clone();
            target.Address = Address.DeepCopy();
        }

        public override string ToString()
        {
            return $"{nameof(Names)}: {string.Join(",", Names)}, " +
                   $"{nameof(Address)}: {Address}";
        }
    }

    public static class ExtensionMethods
    {
        public static T DeepCopy<T> (this IDeepCopyable<T> item)
            where T : new()
        {
            return item.DeepCopy();
        }

        public static T DeepCopy<T> (this T person)
            where T : Person, new()
        {
            return ((IDeepCopyable<T>) person).DeepCopy();
        }
    }
}
