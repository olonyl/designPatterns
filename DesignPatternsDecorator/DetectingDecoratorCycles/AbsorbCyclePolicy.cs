using System;
using System.Collections.Generic;

namespace DesignPatternsDecorator.DetectingDecoratorCycles
{
    public class AbsorbCyclePolicy : ShapeDecoratorCyclePolicy
    {
        public override bool ApplicationAllowed(Type type, IList<Type> allTypes)
        {
            return !allTypes.Contains(type);
        }

        public override bool TypeAdditionAllowed(Type type, IList<Type> allTypes)
        {
            return true;
        }
    }
}
