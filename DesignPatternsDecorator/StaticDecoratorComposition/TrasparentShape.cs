namespace DesignPatternsDecorator.StaticDecoratorComposition
{
    public class TrasparentShape : Shape
    {
        private readonly Shape shape;
        private readonly float transparency;
        public TrasparentShape(Shape shape, float transparency)
        {
            this.shape = shape;
            this.transparency = transparency;
        }

        public override string AsString() => $"{shape.AsString()} with {transparency * 100.0f} %";
    }
}
