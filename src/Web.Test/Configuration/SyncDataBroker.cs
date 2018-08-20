using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Web.Test.Infrastructure.Common.Results;
using Web.Test.Infrastructure.Common.Wrappers;
using Web.Test.Infrastructure.Domain.Contracts.Integration;
using Web.Test.Infrastructure.Domain.Models;
using Web.Test.Infrastructure.Integration.Elastic.Indexes.Queries;
using Web.Test.Infrastructure.Integration.RabbitMQ.Handlers;

namespace Web.Test.Configuration
{
    public static class SyncDataBroker
    {
        private static IServiceProvider provider;
        public static void UseSyncDataBroker (this IApplicationBuilder app)
        {
            provider = app.ApplicationServices;    
            var employeesListener = new BatchMessageHandler<CRUDWrapper<Employee>>("employees",EmployeesBatchHandler, 10);
            var offersListener = new BatchMessageHandler<CRUDWrapper<Offer>>("offers",OffersBatchHandler, 10);

            provider.GetService<IQueueManager>().Listener(employeesListener).Start();
            provider.GetService<IQueueManager>().Listener(offersListener).Start();

            var employeesTimer = new Timer(
                (o) => {
                    employeesListener.FatchMessages();
                },null,  10*1000, 10*1000);  

            var offersTimer = new Timer(
                (o) => {
                    offersListener.FatchMessages();
                },null,  10*1000, 10*1000);                  
        }

        private static VoidResult EmployeesBatchHandler(IEnumerable<CRUDWrapper<Employee>> batch) 
        {
            if(!batch.Any())
                return new VoidResult();
                
            return provider.GetService<IESStorage>().Get<Employee>().Query(new ExecuteEmployeesBatch(batch));
        }

        private static VoidResult OffersBatchHandler(IEnumerable<CRUDWrapper<Offer>> batch) 
        {
             if(!batch.Any())
                return new VoidResult();
            Console.WriteLine("OFFER BATCH:"+batch.Count());     
            return provider.GetService<IESStorage>().Get<Offer>().Query(new ExecuteOffersBatch(batch));
        }
    }
}