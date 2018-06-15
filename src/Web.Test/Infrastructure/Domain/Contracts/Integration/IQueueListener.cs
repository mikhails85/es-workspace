using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.Test.Infrastructure.Domain.Contracts.Integration
{
    public interface IQueueListener
    {
        IQueueManager QueueManager{ get; }        
     
        void Start();
        void Stop();
        
        void SetManager(IQueueManager manager);   
        void RejectMessage(ulong tag);
        void ConfirmMessage(ulong tag);
    }
}