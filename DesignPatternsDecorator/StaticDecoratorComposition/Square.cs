namespace DesignPatternsDecorator.StaticDecoratorComposition
{
    public class Square : Shape
    {
        private float side;
        public Square(float side)
        {
            this.side = side;
        }
        public Square() : this(0.0f)
        {

        }
        public override string AsString() => $"Square with a side of {side}";
    }
}
