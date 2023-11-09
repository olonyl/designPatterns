using Autofac;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace DeclarativeEventSubscription
{
    public interface IEvent { }

    public interface ISend<TEvent> 
        where TEvent : IEvent { 
    event EventHandler<TEvent> Sender;
    }

    public interface IHandle<TEvent>
        where TEvent : IEvent
    { 
        void Handle(object sender, TEvent args);
    }

    public class ButtonPressedEvent : IEvent
    {
        public int NumberOfClicks;
    }
    public class Button : ISend<ButtonPressedEvent>
    {
        public event EventHandler<ButtonPressedEvent> Sender;

        public void Fire(int clicks)
        {
            Sender?.Invoke(this, new ButtonPressedEvent { NumberOfClicks = clicks });
        }
    }

    public class Logging : IHandle<ButtonPressedEvent>
    {
        public void Handle(object sender, ButtonPressedEvent args)
        {
            Console.WriteLine($"The button was clicked {args.NumberOfClicks} times");
        }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            var cb = new ContainerBuilder();
            var ass = Assembly.GetExecutingAssembly();

            cb.RegisterAssemblyTypes(ass)
                .AsClosedTypesOf(typeof(ISend<>))
                .SingleInstance();

            cb.RegisterAssemblyTypes(ass)
                .Where(t => t.GetInterfaces()
                    .Any(i => i.IsGenericType &&
                             i.GetGenericTypeDefinition() == typeof(IHandle<>)
                             ))
                .OnActivated(act =>
                {
                    var instanceType = act.Instance.GetType();
                    var interfaces = instanceType.GetInterfaces();
                    foreach (var i in interfaces)
                    {
                        if (i.IsGenericType &&
                        i.GetGenericTypeDefinition() == typeof(IHandle<>))
                        {
                            var arg0 = i.GetGenericArguments()[0];
                            var senderType = typeof(ISend<>).MakeGenericType(arg0);
                            var allSenderTypes = typeof(IEnumerable<>)
                                .MakeGenericType(senderType);
                            var allServices = act.Context.Resolve(allSenderTypes);
                            foreach (var service in (IEnumerable)allServices)
                            {
                                var eventInfo = service.GetType().GetEvent("Sender");
                                var handleMethod = instanceType.GetMethod("Handle");
                                var handler = Delegate.CreateDelegate(
                                    eventInfo.EventHandlerType, null, handleMethod);
                                eventInfo.AddEventHandler(service, handler);
                            }

                        }
                    }
                }).SingleInstance()
                .AsSelf();

            var container = cb.Build();
            var button = container.Resolve<Button>();
            var loggin = container.Resolve<Logging>();


            button.Fire(1);
            button.Fire(2);
        }
    }
}
