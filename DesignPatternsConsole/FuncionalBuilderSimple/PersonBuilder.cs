using System;
using System.Collections.Generic;
using System.Linq;

namespace DesignPatternsConsole.FunctionalBuilderSimple
{
    public sealed class PersonBuilder
    {
        private readonly List<Func<Person, Person>> actions
            = new List<Func<Person, Person>>();

        public PersonBuilder Called(string name)
            => Do(p => p.Name = name);
        public PersonBuilder Do(Action<Person> action)
            => AddAction(action);
        public Person Build() => actions.Aggregate(new Person(), (p, f) => f(p));

        private PersonBuilder AddAction(Action<Person> action)
        {
            actions.Add(p =>
            {
                action(p);
                return p;
            });
            return this;
        }
    }
}
