using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.Test.Infrastructure.Domain.Contracts.Integration
{
    public interface IQueueHandler
    {
        void OnStarting();
        void OnStarted();
        void HandleMessage(ulong tag, string message);
        void SetListener(IQueueListener listener);
        void OnStoping();
    }
}