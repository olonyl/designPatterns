namespace DesignPatternsDecorator.StaticDecoratorComposition
{
    public class ColoredShape : Shape
    {
        private readonly Shape shape;
        private string color;
        public ColoredShape(Shape shape, string color)
        {
            this.shape = shape;
            this.color = color;
        }
        public override string AsString() => $"{shape.AsString()} with {color}";
    }
}
