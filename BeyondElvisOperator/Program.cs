using System;

namespace BeyondElvisOperator
{
    public static class Maybe
    {
        public static TResult With<TInput, TResult>(this TInput o , 
            Func<TInput, TResult> evaluator)
            where TInput : class
            where TResult : class
        {
            if (o == null) return null;
            else return evaluator(o);
        }

        public static TInput If<TInput> (this TInput o, 
            Func<TInput, bool> evaluator)
            where TInput : class
        {
            if(o == null) return null;
            return evaluator(o) ? o : null;
        }

        public static TInput Do<TInput>(this TInput o , Action<TInput> action)
            where TInput : class
        {
            if (o != null) return null;
            action(o);
            return o;
        }

        public static TResult Return<TInput, TResult>(this TInput o,
            Func<TInput, TResult> evaluator, TResult failureValue)
            where TInput : class
        {
            if(o == null) return failureValue;
            return evaluator(o);
        }

        public static TResult WithValue<TInput, TResult>(this TInput o,
            Func<TInput, TResult> evaluator)
            where TInput : struct
        {
            return evaluator(o);
        }
    }

    public class Person
    {
        public Address Addresss { get; set; }
    }

    public class Address
    {
        public string PostCode { get; set; }
    }

    public class MaybeMonadDemo
    {
        public void MyMethod (Person p)
        {
            string postcode = p.With(x => x.Addresss).With(x => x.PostCode);
            postcode = p.If(HasMediacalRecord)
                .With(x=> x.Addresss)
                .Do(CheckAddress)
                .Return(x=> x.PostCode, "UNKNOWN")
                ;
        }

        private void CheckAddress(Address obj)
        {
            throw new NotImplementedException();
        }

        private bool HasMediacalRecord(Person arg)
        {
            throw new NotImplementedException();
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
        }
    }
}
