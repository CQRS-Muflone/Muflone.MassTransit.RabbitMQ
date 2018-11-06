# Muflone.Eventstore
Muflone repository and event dispatcher for [Event Store](https://eventstore.org "Event store's Homepage")
 
### Usage ###

In your application's startup call:

```csharp
services.AddMufloneMassTransitWithRabbitMQ(serviceBusOptions, x =>
{
  x.AddConsumer<MyFisrtEventConsumer>();  
  x.AddConsumer<MySecondEventConsumer>();
});
```
