using Coding.Exercise;
using System;
using System.Numerics;

namespace StrategyExcercise
{
    internal class Program
    {
        static void Main(string[] args)
        {
            double a =2, b=9, c =10;

            var str1 = new QuadraticEquationSolver(new OrdinaryDiscriminantStrategy());

            Console.WriteLine(str1.Solve(a,b,c));

            var str2 = new QuadraticEquationSolver(new RealDiscriminantStrategy());

            Console.WriteLine(str2.Solve(a,b,c));
        }
    }
}

namespace Coding.Exercise
{
    public interface IDiscriminantStrategy
    {
        double CalculateDiscriminant(double a, double b, double c);
    }

    public class OrdinaryDiscriminantStrategy : IDiscriminantStrategy
    {
        // todo
        public double CalculateDiscriminant(double a, double b, double c)
        {
            var result = Math.Pow(b, 2) - 4 * a * c;
            return result;
        }
    }

    public class RealDiscriminantStrategy : IDiscriminantStrategy
    {
        // todo (return NaN on negative discriminant!)
        public double CalculateDiscriminant(double a, double b, double c)
        {
            var result =Math.Pow(b, 2) - 4 * a * c;
            return result < 0 ? double.NaN: result; 
        }
    }

    public class QuadraticEquationSolver
    {
        private readonly IDiscriminantStrategy strategy;

        public QuadraticEquationSolver(IDiscriminantStrategy strategy)
        {
            this.strategy = strategy;
        }

        public Tuple<Complex, Complex> Solve(double a, double b, double c)
        {
            var disc = new Complex(strategy.CalculateDiscriminant(a, b, c),0);
            var sqrt = Complex.Sqrt(disc);

            return Tuple.Create(
                (-b + sqrt) / (2 * a),
                (-b - sqrt) / (2 * a)
                );
        }
    }
}