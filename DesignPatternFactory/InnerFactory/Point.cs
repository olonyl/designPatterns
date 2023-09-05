using System;

namespace DesignPatternFactory.InnerFactory
{
    public class Point
    {
        private double x, y;
        private Point(double x, double y)
        {
            this.x = x;
            this.y = y;
        }
        public override string ToString()
        {
            return $"{nameof(x)}:{x}, {nameof(y)}:{y}";
        }
        public static class Factory
        {
            public static Point NewCartitianPoint(double x, double y)
            {
                return new Point(x, y);
            }

            public static Point NewPolarPoint(double rho, double theta)
            {
                return new Point(rho * Math.Cos(theta), rho * Math.Sin(theta));
            }
        }
    }
}

