using System;

namespace DesignPatternsConsole.StepwiseBuilder
{
        public class CarBuilder
        {
            private class Impl :
                ISpecifyCarType,
                ISpecifyWheelSize,
                IBuildCar
            {
                private Car car = new Car();
                
                public ISpecifyWheelSize OfType(CarType type)
                {
                    car.Type = type;
                    return this;
                }

                public IBuildCar WithWheels(int size)
                {
                    switch  (car.Type) {
                        case CarType.Crossover when size < 17 || size > 20:
                        case CarType.Sedan when size < 15 || size > 17:
                            throw new ArgumentException($"Wrong size of wheel for {car.Type}");

                    }
                    car.WheelSize= size;
                    return this;
                }
                public Car Build()
                {
                    return car;
                }

            }

            public static ISpecifyCarType Create()
            {
                return new Impl();
            }
        }
}
