using System;
using System.Collections;
using System.Collections.Generic;

namespace DuckTyping
{
    interface IScalar<T> : IEnumerable<T>
    {
        IEnumerator<T> IEnumerable<T>.GetEnumerator() {
            yield return (T)this;
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }

    public class MyClass : IScalar<MyClass>
    {
        public override string ToString()
        {
            return "MyClass";
        }
    }
    internal class Program
    { 
        static void Main(string[] args)
        {
            using var foo = new Foo();
            using var mcd = new MyClassDisp();

           var mc = new MyClass();
            foreach (var x in mc)
                Console.WriteLine(x);
        }
    }

    ref struct Foo
    {
        public void Dispose()
        {
            Console.WriteLine("Disposing Foo");
        }
    }

    interface IMyDisposable<T> : IDisposable
    {
        void IDisposable.Dispose()
        {
            Console.WriteLine($"Disposing {typeof(T).Name}");
        }
    }

    public class MyClassDisp : IMyDisposable<MyClassDisp>
    {

    }
}
