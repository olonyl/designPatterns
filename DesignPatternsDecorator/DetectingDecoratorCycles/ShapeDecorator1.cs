namespace DesignPatternsDecorator.DetectingDecoratorCycles
{
    public abstract class ShapeDecorator<TSelf, TCyclePolity> : ShapeDecorator
        where TCyclePolity : ShapeDecoratorCyclePolicy, new()
    {
        protected readonly TCyclePolity policy = new TCyclePolity();
        protected ShapeDecorator(Shape shape) : base(shape)
        {
            if (policy.TypeAdditionAllowed(typeof(TSelf), types))
                types.Add(typeof(TSelf));
        }
    }
}
