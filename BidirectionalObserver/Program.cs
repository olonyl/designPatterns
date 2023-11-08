using JetBrains.Annotations;
using System;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace BidirectionalObserver
{
    public class Product : INotifyPropertyChanged
    {
        private string name;

        public string Name
        {
            get => name;
            set
            {
                if (value == name) return;
                name = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged(
            [CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this,
                new PropertyChangedEventArgs(propertyName));
        }
        public override string ToString()
        {
            return $"Product : {Name}";
        }
    }
    public class Window : INotifyPropertyChanged
    {
        private string productName;

        public string ProductName
        {
            get => productName;
            set
            {
                if (value == productName) return;
                productName = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged(
            [CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this,
                new PropertyChangedEventArgs(propertyName));
        }

        public override string ToString()
        {
            return $"Window : {ProductName}";
        }
    }

    public sealed class BidirectionalBinding : IDisposable
    {
        private bool disposed;
        
        public BidirectionalBinding(
            INotifyPropertyChanged first, 
            Expression<Func<object>> firstProperty,
            INotifyPropertyChanged second,
            Expression<Func<object>> secondProperty)
        {
            if(firstProperty.Body is MemberExpression firstExpr 
                && secondProperty.Body is MemberExpression secondExpr)
            {
                if(firstExpr.Member is PropertyInfo firstProp 
                    && secondExpr.Member is PropertyInfo secondProp)
                {
                    first.PropertyChanged += (s, e) =>
                    {
                        if (!disposed)
                            secondProp.SetValue(second, firstProp.GetValue(first));
                    };
                    second.PropertyChanged += (s, e) =>
                    {
                        if (!disposed)
                            firstProp.SetValue(first, secondProp.GetValue(second));
                    };
                }
            }
        }

        public void Dispose()
        {
            disposed = true;  
        }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            var product = new Product { Name = "Book" };
            var window = new Window { ProductName = "Book" };

            //product.PropertyChanged += (sender, e) =>
            //{
            //    if (e.PropertyName == nameof(Product.Name))
            //    {
            //        Console.WriteLine("Name changed in Product");
            //        window.ProductName = product.Name;
            //    }
            //};
            //window.PropertyChanged += (sender, e) =>
            //{
            //    if (e.PropertyName == nameof(Window.ProductName))
            //    {
            //        Console.WriteLine("Name changed in Window");
            //        product.Name = window.ProductName;

            //    }
            //};
             var binding = new BidirectionalBinding(
                product,
                () => product.Name,
                window,
                () => window.ProductName);
            
            product.Name = "Smart Book";
            window.ProductName = "Really smart book";

            Console.WriteLine(product);
            Console.WriteLine(window);
        }
    }
}
