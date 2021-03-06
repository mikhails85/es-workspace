using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Web.Test.Infrastructure.Common.Results;
using Web.Test.Infrastructure.Common.Wrappers;
using Web.Test.Infrastructure.Domain.Contracts.Integration;
using Web.Test.Infrastructure.Domain.Models;

namespace Web.Test.Infrastructure.Integration.Elastic.Indexes.Queries
{
    public class ExecuteEmployeesBatch :VoidResult, IESQuery<Employee>
    {
        private readonly IEnumerable<CRUDWrapper<Employee>> actions;

        public ExecuteEmployeesBatch(IEnumerable<CRUDWrapper<Employee>> actions)
        {
            this.actions = actions;
        }

        public void Execute(IESIndex<Employee> context)
        {
            var toDelete = this.actions.Where(x=>x.Action == CRUDActionType.Delete).Select(x=>x.Entity).ToList();
            var toCreate = this.actions.Where(x=>x.Action == CRUDActionType.Create).Select(x=>x.Entity).ToList();
            var toUpdate = this.actions.Where(x=>x.Action == CRUDActionType.Update).Select(x=>x.Entity).ToList();

            var client = context.GetClient();
            client.Bulk(x=>x.IndexMany(toCreate,(a,b)=>{return a.Id(b.Id);})
                            .UpdateMany(toUpdate,(a,b)=>{return a.Id(b.Id);})
                            .DeleteMany(toDelete,(a,b)=>{return a.Id(b.Id);}));
        }
    }
}