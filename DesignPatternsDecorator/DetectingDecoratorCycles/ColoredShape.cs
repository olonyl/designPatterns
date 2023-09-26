using System.Linq;
using System.Text;

namespace DesignPatternsDecorator.DetectingDecoratorCycles
{
    public class ColoredShape
        : ShapeDecorator<ColoredShape, AbsorbCyclePolicy>
    {
        private readonly string color;

        public ColoredShape(Shape shape, string color) : base(shape)
        {
            this.color = color;
            this.shape = shape;
        }

        public override string AsString()
        {
            var sb = new StringBuilder($"{shape.AsString()}");
            if (policy.ApplicationAllowed(types[0], types.Skip(1).ToList()))
                sb.Append($" has the color {color}");

            return sb.ToString();
        }
    }
}
