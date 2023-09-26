using System;
using System.Collections.Generic;

namespace DesignPatternsDecorator.DetectingDecoratorCycles
{
    public class CycleAllowedPolicity : ShapeDecoratorCyclePolicy
    {
        public override bool ApplicationAllowed(Type type, IList<Type> allTypes)
        {
            return true;
        }

        public override bool TypeAdditionAllowed(Type type, IList<Type> allTypes)
        {
            return true;
        }
    }
}
