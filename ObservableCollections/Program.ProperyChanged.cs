using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace ObserverViaSpecialInterface
{
    public class Event
    {

    }

    public class FallsIllEvent : Event
    {
        public string Address;
    }

    public class Person : IObservable<Event>
    {
        private readonly HashSet<Subscription> subscriptions
            = new HashSet<Subscription>();

        public IDisposable Subscribe(IObserver<Event> observer)
        {
            var subscription = new Subscription(this, observer);
            subscriptions.Add(subscription);
            return subscription;
        }

        public void FallsIll()
        {
            foreach (var s in subscriptions)
            {
                s.Observer.OnNext(new FallsIllEvent { Address = "123 London Rd" });
            }
        }

        private class Subscription : IDisposable
        {
            private readonly Person person;
            public readonly IObserver<Event> Observer;
            public Subscription(Person person, IObserver<Event> observer)
            {
                this.person = person;
                Observer = observer;
            }
            public void Dispose()
            {
                person.subscriptions.Remove(this);
            }
        }
    }

    public class Program : IObserver<Event>
    {
        static void AnotherMain(string[] args)
        {
            new Program();
        }

        public Program()
        {
            var person = new Person();
            var sub = person.Subscribe(this);
            person.FallsIll();
        }
        public void OnCompleted() { }

        public void OnError(Exception error) { }

        public void OnNext(Event value)
        {
            if (value is FallsIllEvent args)
                Console.WriteLine($"A doctor is required at {args.Address}");
        }
    }
}
