// See https://aka.ms/new-console-template for more information
using Events.Consumer;

Console.WriteLine("Hello, World!");
var consumer = new PermissionOperationConsumer();
var cts = new CancellationTokenSource();
await consumer.StartAsync(cts.Token);