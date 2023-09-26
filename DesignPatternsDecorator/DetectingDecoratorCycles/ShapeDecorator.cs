using System;
using System.Collections.Generic;

namespace DesignPatternsDecorator.DetectingDecoratorCycles
{
    public abstract class ShapeDecorator : Shape
    {
        protected internal readonly List<Type> types = new List<Type>();
        protected internal Shape shape;

        public ShapeDecorator(Shape shape)
        {
            this.shape = shape;
            if (shape is ShapeDecorator sd)
            {
                types.AddRange(sd.types);
            }
        }
    }
}
