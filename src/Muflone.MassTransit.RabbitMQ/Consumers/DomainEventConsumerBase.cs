using System.Threading.Tasks;
using MassTransit;
using Muflone.Messages.Events;

namespace Muflone.MassTransit.RabbitMQ.Consumers
{
  public abstract class DomainEventConsumerBase<TEvent> : IConsumer<TEvent> where TEvent : DomainEvent
  {
    protected abstract IDomainEventHandler<TEvent> Handler { get; }
    public abstract Task Consume(ConsumeContext<TEvent> context);
  }
}