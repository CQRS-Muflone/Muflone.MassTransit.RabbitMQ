using System;
using System.Threading;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Muflone.Messages;
using Muflone.Messages.Commands;

namespace Muflone.MassTransit.RabbitMQ
{
  public class ServiceBus : IHostedService, IServiceBus, IEventBus
  {
    private readonly IBusControl busControl;
    private readonly string commandQueue;
    private readonly ILogger<ServiceBus> logger;

    public ServiceBus(IBusControl busControl, ILogger<ServiceBus> logger, IOptions<ServiceBusOptions> options)
    {
      this.busControl = busControl ?? throw new ArgumentNullException(nameof(busControl));
      this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
      if (options?.Value == null)
        throw new ArgumentNullException(nameof(options));
      commandQueue = options.Value.BrokerUrl;
      if (!commandQueue.EndsWith("/"))
        commandQueue += "/";
      commandQueue += options.Value.QueueNameCommand;
    }

    public async Task Publish(IMessage @event)
    {
      try
      {
        logger.LogInformation($"ServiceBus: Publishing event {@event.GetType()}");
        await busControl.Publish(@event, @event.GetType());
      }
      catch (Exception e)
      {
        logger.LogError($"ServiceBus: Error publishing event {@event.GetType()}, Message: {e.Message}, StackTrace: {e.StackTrace}");
        throw;
      }
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
      return busControl.StartAsync(cancellationToken);
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
      return busControl.StopAsync(cancellationToken);
    }

    public virtual async Task Send<T>(T command) where T : class, ICommand
    {
      try
      {
        logger.LogInformation($"ServiceBus: Sending command {command.GetType()} AggregateId: {command.AggregateId} to queue {commandQueue}");
        var endPoint = await busControl.GetSendEndpoint(new Uri(commandQueue));
        await endPoint.Send(command);
      }
      catch (Exception e)
      {
        logger.LogError($"ServiceBus: Error sending command {command.GetType()} AggregateId: {command.AggregateId} to queue {commandQueue}, Message: {e.Message}, StackTrace: {e.StackTrace}");
        throw;
      }
    }

    [Obsolete("With MassTransit, handlers must be registered in the constructor")]
    public Task RegisterHandler<T>(Action<T> handler) where T : IMessage
    {
      throw new Exception("With MassTransit, handlers must be registered in the constructor");
    }
  }
}