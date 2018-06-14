using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Web.Test.Infrastructure.Domain.Contracts.Integration;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace Web.Test.Infrastructure.Integration.RabbitMQ
{
    public class MQListener : IQueueListener
    {
        private readonly MQSettings settings;
        private readonly IQueueHandler handler;
        
        private IConnection connection;        
        private IModel channel;
        
        public IModel Channel => channel;
        public IQueueManager QueueManager {get;set;}
        
        
        public MQListener(MQSettings settings, IQueueHandler handler)
        {
            this.settings = settings;
            this.handler = handler;            
            this.handler.SetListener(this);            
        }
        
        public void SetManager(IQueueManager manager)
        {
            QueueManager = manager;
        }
        
        public void Start()
        {
            var factory = new ConnectionFactory() { HostName = settings.Host };
            this.connection = factory.CreateConnection();
            this.channel = connection.CreateModel();
            
            this.handler.OnStarting();
                        
            var consumer = new EventingBasicConsumer(this.channel);
            consumer.Received += (s,e)=>{                
                var body = e.Body;
                var message = Encoding.UTF8.GetString(body);            
                this.handler.HandleMessage(e.DeliveryTag, message);
            };     
            
            this.handler.OnStarted();
        }
        
        public void Stop()
        {
            this.handler.OnStoping();
            
            this.channel.Close(200, "Goodbye");
            this.connection.Close();
        }
    }
}