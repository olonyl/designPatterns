using System;

namespace DesignPatternFactory.FactoryMethod
{
    class Point
    {
        private double x;
        private double y;
        public static Point NewCartitianPoint(double x, double y)
        {
            return new Point(x, y);
        }

        public static Point NewPolarPoint(double rho, double theta)
        {
            return new Point(rho * Math.Cos(theta), rho * Math.Sin(theta));
        }
        private Point(double x, double y )
        {       
            this.x = x;
            this.y = y;        
        }
        public override string ToString()
        {
            return $"{nameof(x)}:{x}, {nameof(y)}:{y}";
        }
    }
}
