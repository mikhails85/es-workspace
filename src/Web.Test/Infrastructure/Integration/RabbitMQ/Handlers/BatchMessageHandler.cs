using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Web.Test.Infrastructure.Domain.Contracts.Integration;

namespace Web.Test.Infrastructure.Integration.RabbitMQ.Handlers
{
    public class BatchMessageHandler:IQueueHandler
    {
        public void OnStarting()
        {
            
        }
        
        public void OnStarted()
        {
            
        }
        
        public void HandleMessage(ulong tag, string message)
        {
            
        }
        
        public void SetListener(IQueueListener listener)
        {
            
        }
        
        public void OnStoping()
        {
            
        }
    }
}