using System;

namespace DesignPatternsPrototypes.Cloning
{
    public class Person : IPrototype<Person>, ICloneable
    {
        public string[] Names;
        public Address Address;
        public Person(string[] names, Address address)
        {
            if (names == null)
            {
                throw new ArgumentNullException(paramName: nameof(names));
            }

            if (address == null)
            {
                throw new ArgumentNullException(paramName: nameof(address));
            }
            Names = names;
            Address = address;
        }

        public Person(Person other)
        {
            Names = other.Names;
            Address = new Address(other.Address);
        }

        public override string ToString()
        {
            return $"{nameof(Names)}: {string.Join(" ", Names)}, {nameof(Address)}: {Address}";
        }

        public Person DeepCopy()
        {
            return new Person(Names, Address.DeepCopy());
        }

        public object Clone()
        {
            return new Person(Names, (Address)Address.Clone());
        }
    }
}
