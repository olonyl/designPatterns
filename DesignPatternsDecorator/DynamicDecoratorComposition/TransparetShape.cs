namespace DesignPatternsDecorator.DynamicDecoratorComposition
{
    public class TransparetShape : IShape
    {
        private float transparency;
        private IShape shape;
        public TransparetShape(float transparency, IShape shape)
        {
            this.transparency = transparency;
            this.shape = shape;
        }

        public string AsString() => $"{shape.AsString()} has {transparency * 100}% transparency";
    }
}
