using System;

namespace DesignPatternsAdapterExcercise
{
    public class Square
    {
        public int Side;
    }

    public interface IRectangle
    {
        int Width { get; }
        int Height { get; }
    }

    public static class ExtensionMethods
    {
        public static int Area(this IRectangle rc)
        {
            return rc.Width * rc.Height;
        }
    }

    public class Rectangle : IRectangle
    {
        public int Width => throw new NotImplementedException();

        public int Height => throw new NotImplementedException();
    }

    public class SquareToRectangleAdapter : IRectangle
    {
        private IRectangle _rectangle;
        private int _side;
        public SquareToRectangleAdapter(Square square)
        {
            _side = square.Side;
        }

        public int Width => _side;

        public int Height => _side;
        // todo
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
        }
    }
}
