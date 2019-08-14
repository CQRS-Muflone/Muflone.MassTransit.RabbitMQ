using System.Threading.Tasks;
using MassTransit;
using Muflone.Messages.Events;

namespace Muflone.MassTransit.RabbitMQ.Consumers
{
  public abstract class IntegrationEventConsumerBase<TEvent> : IConsumer<TEvent> where TEvent : IntegrationEvent
  {
    protected abstract IIntegrationEventHandler<TEvent> Handler { get; }
    public abstract Task Consume(ConsumeContext<TEvent> context);
  }
}