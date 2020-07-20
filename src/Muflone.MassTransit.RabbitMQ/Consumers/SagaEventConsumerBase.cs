using System.Threading.Tasks;
using MassTransit;
using Muflone.Messages.Events;
using Muflone.Saga;

namespace Muflone.MassTransit.RabbitMQ.Consumers
{
	public abstract class SagaEventConsumerBase<TEvent> : IConsumer<TEvent> where TEvent : Event
	{
		protected abstract ISagaEventHandler<TEvent> Handler { get; }
		public abstract Task Consume(ConsumeContext<TEvent> context);
	}
}