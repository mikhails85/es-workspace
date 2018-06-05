using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;
using System.Threading;

namespace MQ.Tests
{
    public class Client
    {

        public void GetMessage()
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: "hello",
                                     durable: false,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: null);
                                     
                channel.BasicQos(prefetchSize: 0, prefetchCount: 5, global: false);
                var consumer = new QueueingBasicConsumer(channel);
                channel.BasicConsume("hello", false, consumer);
                
                var startTime = DateTime.Now;
                var count = 0;
                while (count < 5)
                {
                    var ea = new BasicDeliverEventArgs();
                    if (consumer.Queue.Dequeue(5000, out ea))
                    {
                        var body = ea.Body;
                        var message = Encoding.UTF8.GetString(body);
                        count++;                        
                        channel.BasicAck(ea.DeliveryTag, false);
                        Console.WriteLine($"Message:{message}");
                    }
                    else {
                        break;
                    }
                    
                    if ((DateTime.Now - startTime) >= TimeSpan.FromMilliseconds(5000))
                        break;
                }
            }
        }
    }
}