using System.Threading.Tasks;
using MassTransit;
using Muflone.Messages.Commands;

namespace Muflone.MassTransit.RabbitMQ.Consumers
{
  public abstract class CommandConsumerBase<TCommand> : IConsumer<TCommand> where TCommand : Command
  {
    protected abstract ICommandHandler<TCommand> Handler { get; }
    public abstract Task Consume(ConsumeContext<TCommand> context);
  }
}