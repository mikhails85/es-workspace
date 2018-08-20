using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Web.Test.Infrastructure.Common.Results;
using Web.Test.Infrastructure.Common.Wrappers;
using Web.Test.Infrastructure.Domain.Contracts.Integration;
using Web.Test.Infrastructure.Domain.Models;
using Web.Test.Infrastructure.Integration.Elastic.Indexes.Models;

namespace Web.Test.Infrastructure.Integration.Elastic.Indexes.Queries
{
    public class ExecuteOffersBatch :VoidResult, IESQuery<Offer>
    {
        private readonly IEnumerable<CRUDWrapper<Offer>> actions;

        public ExecuteOffersBatch(IEnumerable<CRUDWrapper<Offer>> actions)
        {
            this.actions = actions;
        }

        public void Execute(IESIndex<Offer> context)
        {
            var toDelete = this.actions.Where(x=>x.Action == CRUDActionType.Delete).Select(x=>(ESOffer)x.Entity).ToList();
            var toCreate = this.actions.Where(x=>x.Action == CRUDActionType.Create).Select(x=>(ESOffer)x.Entity).ToList();
            
            Console.WriteLine("OFFER BATCH CREATE:"+toCreate.Count());
            
            var toUpdate = this.actions.Where(x=>x.Action == CRUDActionType.Update).Select(x=>(ESOffer)x.Entity).ToList();

            var client = context.GetClient();
            var result = client.Bulk(x => {
                if(toCreate.Any())                
                    x=x.IndexMany(toCreate,(a,b)=>{return a.Id(b.Id);});

                if(toUpdate.Any())
                    x=x.UpdateMany(toUpdate,(a,b)=>{return a.Id(b.Id);});
                    
                 if(toDelete.Any())
                    x=x.DeleteMany(toDelete,(a,b)=>{return a.Id(b.Id);});    
                    
                return x;
            });
            if (!result.IsValid)
            {
               Console.WriteLine("OFFER BATCH ERROR:"+result.DebugInformation);
                return;    
            }
        }
    }
}