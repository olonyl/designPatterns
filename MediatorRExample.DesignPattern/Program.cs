using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Autofac;
using JetBrains.Annotations;
using MediatR;

namespace MediatorRExample.DesignPattern
{
    public class PongResponse
    {
        public DateTime Timestamp;

        public PongResponse(DateTime timestamp)
        {
            Timestamp = timestamp;
        }
    }
    public class PingCommand : IRequest<PongResponse>
    {

    }

    [UsedImplicitly]
    public class PingCommandHandler : IRequestHandler<PingCommand, PongResponse>
    {
        public async Task<PongResponse> Handle(PingCommand request, CancellationToken cancellationToken)
        {
            return await Task.FromResult(new PongResponse(DateTime.UtcNow))
                .ConfigureAwait(false);
        }
    }
    internal class Program
    {
        static async Task Main(string[] args)
        {
           //await ExecuteMediator();

           var m = new Coding.Exercise.Mediator();
           var p1 = new Coding.Exercise.Participant(m);
           var p2 = new Coding.Exercise.Participant(m);

           p1.Say(3);
            
        }

        static async Task ExecuteMediator()
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<Mediator>()
                .As<IMediator>()
                .InstancePerLifetimeScope();

            builder.Register<ServiceFactory>(ctx =>
            {
                var c = ctx.Resolve<IComponentContext>();
                return t => c.Resolve(t);
            });

            builder.RegisterAssemblyTypes(typeof(Program).Assembly)
                .AsImplementedInterfaces();

            var container = builder.Build();
            var mediator = container.Resolve<IMediator>();
            var response = await mediator.Send(new PingCommand());

            Console.WriteLine($"we got a response at {response.Timestamp}");
        }
    }


}

namespace Coding.Exercise
{
    public class Participant
    {
        public int Value { get; set; }
        private Mediator mediator;
        public Guid Id { get; } = Guid.NewGuid();

        public Participant(Mediator mediator)
        {
            this.mediator = mediator;

            mediator.Join(this);
            Console.WriteLine($"Participant ${this.Id} created with value {this.Value}");
        }

        public void Say(int n)
        {
            mediator.Say(n, Id);
        }
    }

    public class Mediator
    {
        private List<Participant> participants = new List<Participant>();

        public void Join(Participant participant)
        {
            participants.Add(participant);
        }

        public void Say(int n, Guid id)
        {
            foreach (var participant in participants.Where(w=> w.Id != id))
            {
                participant.Value += n;
                Console.WriteLine($"Participant {participant.Id} has value {participant.Value}");
            }
        }
    }
}
