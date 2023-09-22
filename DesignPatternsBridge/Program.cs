using Autofac;
using DesignPatternBridgeExample;
using System;

namespace DesignPatternBridgeExample
{
    public interface IRenderer
    {
        string WhatToRenderAs { get; }
    }
    public class VectorRender : IRenderer
    {
        public string WhatToRenderAs => "Drawing {Name} as lines";
    }
  
    public class RasterRender : IRenderer
    {
        public string WhatToRenderAs => "Drawing {Name} as pixels";
    }

    public abstract class Shape 
    {
        public string Name { get; set; }
        protected IRenderer renderer;
        public Shape(IRenderer renderer)
        {
            this.renderer = renderer ?? throw new ArgumentNullException(paramName: nameof(renderer));
        }
        public override string ToString()
        {
            return $"{renderer.WhatToRenderAs.Replace("{Name}",Name)}";
        }
    }

    public class Triangle : Shape
    {
        public Triangle(IRenderer renderer):base(renderer) => Name = "Triangle";
        public override string ToString()
        {
            return base.ToString();
        }
    }

    public class Square : Shape
    {
        public Square(IRenderer renderer) : base(renderer) => Name = "Square";
        public override string ToString()
        {
            return base.ToString();
        }

    }
}
namespace DesignPatternsBridge
{
 
    internal class Program
    {
        static void Main(string[] args)
        {
            //    SimpleWay();

            //    UsingContainerBuilder();


            var ex =new Triangle(new RasterRender()).ToString();

            Console.WriteLine(ex);
        }

        //private static void UsingContainerBuilder()
        //{
        //    var cb = new ContainerBuilder();
        //    cb.RegisterType<VectorRenderer>().As<IRenderer>()
        //        .SingleInstance();
        //    cb.Register((c, p) => new Circle(c.Resolve<IRenderer>(),
        //        p.Positional<float>(0)));

        //    using (var c = cb.Build())
        //    {
        //        var circle = c.Resolve<Circle>(new PositionalParameter(0, 5.0f));

        //        circle.Draw();
        //        circle.Resize(10);
        //        circle.Draw();
        //    }
        //}

        //private static void SimpleWay()
        //{
        //    //IRenderer renderer = new RasterRenderer();
        //    IRenderer renderer = new VectorRenderer();
        //    var circle = new Circle(renderer, 5);

        //    circle.Draw();
        //    circle.Resize(10);
        //    circle.Draw();
        //}
    }
}
