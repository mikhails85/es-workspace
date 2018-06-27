using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Web.Test.Infrastructure.Common.Results;
using Web.Test.Infrastructure.Domain.Contracts.Integration;
using Web.Test.Infrastructure.Domain.Models;

namespace Web.Test.Infrastructure.Integration.Elastic.Indexes.Queries
{
    public class InsertBatchEmployees :VoidResult, IESQuery<Employee>
    {
        public void Execute(IESIndex<Employee> context)
        {
            var client = context.GetClient();

        }
    }
}