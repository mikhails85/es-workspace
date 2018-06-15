using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.Test.Infrastructure.Integration.RabbitMQ
{
    public class MQMessage<TMessage>
    {
        public ulong Tag {get;set;}
        public TMessage Message {get;set;}
    }
}