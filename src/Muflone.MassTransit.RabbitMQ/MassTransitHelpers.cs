using System;
using GreenPipes;
using MassTransit;
using MassTransit.ExtensionsDependencyInjectionIntegration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Muflone.MassTransit.RabbitMQ
{
  public static class MassTransitHelpers
  {
    public static IServiceCollection AddMufloneMassTransitWithRabbitMQ(this IServiceCollection services, ServiceBusOptions options, Action<IServiceCollectionConfigurator> consumers)
    {
      services.AddMassTransit(consumers);
      services.AddSingleton(provider => Bus.Factory.CreateUsingRabbitMq(cfg =>
      {
        var host = cfg.Host(new Uri(options.BrokerUrl), h =>
        {
          h.Username(options.Login);
          h.Password(options.Password);
        });

        cfg.ReceiveEndpoint(host, options.QueueName, e =>
        {
          e.PrefetchCount = 16;
          e.UseMessageRetry(x => x.Interval(2, 100));
          e.LoadFrom(provider);
        });
      }));
      services.AddSingleton<IServiceBus, ServiceBus>();
      services.AddSingleton<IEventBus, ServiceBus>();
      services.AddSingleton<IHostedService, ServiceBus>();

      return services;
    }
  }
}