# Muflone.Eventstore
Muflone repository and event dispatcher for [Event Store](https://eventstore.org "Event store's Homepage")
 
### Install ###
`Install-Package Muflone.MassTransit.RabbitMQ`

### Usage ###

In your application's startup call:

```csharp
services.AddMufloneMassTransitWithRabbitMQ(serviceBusOptions, x =>
{
  x.AddConsumer<MyFisrtEventConsumer>();  
  x.AddConsumer<MySecondEventConsumer>();
});
```

### Sample ###
Look at [this repo](https://github.com/Iridio/CQRS-ES_testing_workshop), while we prepare a more specific one
