namespace DesignPatternsConsole.StepwiseBuilder
{       public interface ISpecifyCarType
        {
            ISpecifyWheelSize OfType(CarType type);
        }
}
