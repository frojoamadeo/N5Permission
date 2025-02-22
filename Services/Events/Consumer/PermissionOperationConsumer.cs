//using Confluent.Kafka;
//using Domain.Events.Models;
//using Microsoft.Extensions.Hosting;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Text.Json;
//using System.Threading.Tasks;

//namespace Infraestructure.Events.Consumer
//{
//    public sealed class PermissionOperationConsumer : IHostedService
//    {
//        private readonly string topic = "permission";
//        private readonly string groupId = "dummyGroup";
//        private readonly string bootstrapServers = "localhost:9092";

//        public Task StartAsync(CancellationToken cancellationToken)
//        {
//            var config = new ConsumerConfig
//            {
//                GroupId = groupId,
//                BootstrapServers = bootstrapServers,
//                AutoOffsetReset = AutoOffsetReset.Earliest
//            };

//            try
//            {
//                using (var consumerBuilder = new ConsumerBuilder<Ignore, string>(config).Build())
//                {
//                    consumerBuilder.Subscribe(topic);
//                    var cancelToken = new CancellationTokenSource();

//                    try
//                    {
//                        while (true)
//                        {
//                            var consumer = consumerBuilder.Consume(cancelToken.Token);
//                            var permissionOperation = JsonSerializer.Deserialize<PermissionOperationModel>(consumer.Message.Value);
//                            Console.WriteLine($"Processing PermissionOperation Id:{permissionOperation?.Id}");

//                        }
//                    }
//                    catch (OperationCanceledException)
//                    {
//                        consumerBuilder.Close();
//                    }
//                }
//            }
//            catch (Exception ex)
//            {
//                Console.WriteLine(ex.Message);
//            }

//            return Task.CompletedTask;
//        }

//        public Task StopAsync(CancellationToken cancellationToken)
//        {
//            return Task.CompletedTask;
//        }
//    }
//}
