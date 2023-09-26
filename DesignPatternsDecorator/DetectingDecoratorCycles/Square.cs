namespace DesignPatternsDecorator.DetectingDecoratorCycles
{
    public class Square : Shape
    {
        private float side;

        public Square(float side)
        {
            this.side = side;
        }

        public override string AsString() => $"A square with side {side}";
    }
}
