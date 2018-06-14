using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace MQ.Tests
{
    public class EventClient
    {
        private readonly ClientSettings settings;
        
        private IConnection connection;
        private IModel channel;
        
        public EventClient(ClientSettings settings)
        {
            this.settings = settings;            
        }
        
        public void Start(string queue, Action<EventClient,ulong,string> handler)
        {
            Connect(queue);
            var consumer = new EventingBasicConsumer(this.channel);
            consumer.Received += (s,e)=>{                
                var body = e.Body;
                var message = Encoding.UTF8.GetString(body);            
                handler(this, e.DeliveryTag, message);
            };            
            channel.BasicConsume(queue, false, consumer);
        }
        
        public void Acknowledge(ulong tag)
        {
            channel.BasicAck(tag, false);
        }

        public void Stop()
        {
            this.channel.Close(200, "Goodbye");
            this.connection.Close();
        }
        
        private void Connect(string queue)
        {
            var factory = new ConnectionFactory() { HostName = settings.Host };
            this.connection = factory.CreateConnection();
            this.channel = connection.CreateModel();
            this.channel.QueueDeclare(queue: queue,
                                     durable: false,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: null);
                                     
            this.channel.BasicQos(prefetchSize: 0, prefetchCount: settings.PrefetchCount, global: false);
        }
    }
    
    public class ClientSettings
    {
        public string Host {get;set;} 
        public ushort PrefetchCount {get;set;}         
    }
    
    public class Message{
        public string Value {get;set;}
        public ulong Tag {get;set;}
        public EventClient Client {get;set;}
    }
    
    public class Batch{
        public string Name ="hello";
        
        private List<Message> messages = new List<Message>();
        
        public Action<List<string>> Handler;
                
        public void Add(EventClient client, ulong tag, string message)
        {
            
            messages.Add(new Message{
                Value = message,
                Tag = tag,
                Client = client
            });            
            
            if(messages.Count()>=10)
            {
                Handler(messages.Select(x=>x.Value).ToList());   
                
                foreach(var msg in messages)
                {
                    msg.Client.Acknowledge(msg.Tag);
                }
                
                messages.Clear();
            }
        }
    }
}