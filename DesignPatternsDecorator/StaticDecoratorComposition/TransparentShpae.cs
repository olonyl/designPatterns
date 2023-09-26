namespace DesignPatternsDecorator.StaticDecoratorComposition
{
    public class TransparentShpae<T> : Shape
       where T : Shape, new()
    {
        private T shape = new T();
        private double transparency;

        public TransparentShpae() : this(0)
        {
        }
        public TransparentShpae(double transparency)
        {
            this.transparency = transparency;
        }

        public override string AsString() => $"{shape.AsString()} has {transparency * 100}%";
    }
}
