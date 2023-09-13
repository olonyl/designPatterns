﻿using MoreLinq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http.Headers;

namespace DesignPatternsAdapterVectorRaster
{
    public class Point
    {
        public int X, Y;

        public Point(int x, int y)
        {
            X = x;
            Y = y;  
        }
        protected bool Equals(Point other)
        {
            return X == other.X && Y == other.Y;
        }
        public override bool Equals(object obj)
        {
            if(ReferenceEquals(null, obj)) return false;
            if(ReferenceEquals(this, obj)) return true;
            if(obj.GetType() != typeof(Point)) return false;
            return Equals((Point)obj);
        }
        public override int GetHashCode()
        {
            unchecked
            {
                return (X * 397) ^ Y;
            }
        }
    }

    public class Line
    {
        public Point Start, End;

        public Line(Point start, Point end)
        {
            if(start == null)
            {
                throw new ArgumentNullException(paramName: nameof(start));
            }
            if(end == null)
            {
                throw new ArgumentNullException(paramName: nameof(end));
            }

            Start = start;
            End = end;
        }
        protected bool Equals(Line other)
        {
            return Equals(Start, other.Start) && Equals(End, other.End);
        }

        public override bool Equals(object obj)
        {
         if(ReferenceEquals(null, obj)) return false;
         if(ReferenceEquals(this, obj)) return true;
         if(obj.GetType() != this.GetType()) return false;
         return Equals((Line)obj);
        }
        public override int GetHashCode()
        {
            unchecked
            {
                return ((Start != null ? Start.GetHashCode() : 0) * 397) ^ (End != null ? End.GetHashCode() : 0); 
            }
        }
    }

    public class VectorObject: Collection<Line>
    {

    }

    public class VectorRectangle : VectorObject
    {
        public VectorRectangle(int x, int y, int width, int height)
        {
            Add(new Line(new Point(x , y), new Point(x + width, y)));
            Add(new Line(new Point(x + width,  y), new Point(x + width, y + height)));
            Add(new Line(new Point(x , y), new Point(x , y + height)));
            Add(new Line(new Point(x , y + height), new Point(x + width, y + height)));
        }
    }
    public class LineToPointAdapter : IEnumerable<Point>
    {
        private static int count;
        private static Dictionary<int, List<Point>> cache
            = new Dictionary<int, List<Point>>();

        public LineToPointAdapter(Line line)
        {
            var hash = line.GetHashCode();
            if (cache.ContainsKey(hash)) return;

            Console.WriteLine($"{++count}: Generating point for line [{line.Start.X}, {line.Start.Y}]-[{line.End.X}, {line.End.Y}]");
            
            var points = new List<Point>();

            int left = Math.Min(line.Start.X, line.End.X);
            int right = Math.Max(line.Start.X, line.End.X);
            int top = Math.Min(line.Start.Y, line.End.Y);
            int bottom = Math.Max(line.Start.Y, line.End.Y);
            int dx = right - left;
            int dy = line.End.Y - line.Start.Y;

            if(dx == 0)
            {
                for (int y = top; y <= bottom; ++y) {
                    points.Add(new Point(left, y));
                }
            }
            else if( dy == 0)
            {
                for(int x = left; x <= right; ++x)
                {
                    points.Add(new Point(x, top));
                }
            }

            cache.Add(hash, points);
        }

        public IEnumerator<Point> GetEnumerator()
        {
            return cache.Values.SelectMany(x => x).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
    internal class Program
    {
        private static readonly List<VectorObject> vectorObjects
            = new List<VectorObject>
            {
                new VectorRectangle(1,1,10,10),
                new VectorRectangle(3,3,6,6),
            };

        public static void DrawPoint(Point p)
        {
            Console.Write(".");
        }
        static void Main(string[] args)
        {
            Draw();
            Draw();
        }

        private static void Draw()
        {
            foreach (var vo in vectorObjects)
            {
                foreach (var line in vo)
                {
                    var adapter = new LineToPointAdapter(line);
                    adapter.ForEach(DrawPoint);
                }
            }
        }
    }
}
