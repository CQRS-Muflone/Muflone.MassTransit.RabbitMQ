using System.Threading.Tasks;
using MassTransit;
using Muflone.Messages.Commands;
using Muflone.Saga;

namespace Muflone.MassTransit.RabbitMQ.Consumers
{
  public abstract class SagaStartedByConsumerBase<TCommand> : IConsumer<TCommand> where TCommand : Command
  {
    protected abstract ISagaStartedBy<TCommand> Handler { get; }
    public abstract Task Consume(ConsumeContext<TCommand> context);
  }
}