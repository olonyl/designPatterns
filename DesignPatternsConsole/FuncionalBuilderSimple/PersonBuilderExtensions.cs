namespace DesignPatternsConsole.FunctionalBuilderSimple
{
    public static class PersonBuilderExtensions
    {
        public static PersonBuilder WorksAs(this PersonBuilder builder, string position)
            => builder.Do(p => p.Position = position);
    }
}
