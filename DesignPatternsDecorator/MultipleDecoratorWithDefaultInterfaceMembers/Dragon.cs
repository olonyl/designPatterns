namespace DesignPatternsDecorator.MultipleDecoratorWithDefaultInterfaceMembers
{
    public class Dragon : Organism, ICreature, IBird, ILizar
    {
        public int Age { get; set; }
    }
}
