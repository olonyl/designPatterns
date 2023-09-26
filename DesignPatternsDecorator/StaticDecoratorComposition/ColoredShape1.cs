namespace DesignPatternsDecorator.StaticDecoratorComposition
{
    public class ColoredShape<T> : Shape
        where T : Shape, new()
    {
        private T shape = new T();
        private string color;

        public ColoredShape() : this("Black")
        {
        }
        public ColoredShape(string color)
        {
            this.color = color;
        }

        public override string AsString() => $"{shape.AsString()} with {color}";
    }
}
