using AppServices.Interfaces.IEvents;
using Confluent.Kafka;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Events.Producer
{
    public sealed class PermissionOperationProducer : IPermissionOperationProducer
    {
        private readonly string server = "localhost:9092";
        private readonly string topic = "permission";

        public async Task<bool> SendPermissionOperationToTopic(string message)
        {
            ProducerConfig config = new ProducerConfig
            {
                BootstrapServers = server,
                ClientId = Dns.GetHostName()
            };

            try
            {
                using (var producer = new ProducerBuilder<Null, string>(config).Build())
                {
                    var result = await producer.ProduceAsync(topic, new Message<Null, string>
                    {
                        Value = message
                    });

                    Console.WriteLine($"Delivery at:{result.Timestamp.UtcDateTime}");

                    return await Task.FromResult(true);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error occurred: {ex.Message}");
            }

            return await Task.FromResult(false);
        }
    }
}
