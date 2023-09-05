namespace DesignPatternsConsole.FluentBuilderInheritanceWithRecursiveGenerics
{       public class PersonJobBuilder<SELF>
            : PersonInfoBuilder<PersonJobBuilder<SELF>>
            where SELF : PersonJobBuilder<SELF>
        {
            public SELF WorksAs(string position)
            {
                person.Position = position;
                return (SELF)this;
            }
        }
}
