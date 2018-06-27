using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Web.Test.Infrastructure.Common.Results;
using Web.Test.Infrastructure.Domain.Contracts.Integration;
using Web.Test.Infrastructure.Domain.Models;

namespace Web.Test.Infrastructure.Integration.Elastic.Indexes.Queries
{
    public class InsertBatchOffers :VoidResult, IESQuery<Offer>
    {
        public void Execute(IESIndex<Offer> context)
        {
            var client = context.GetClient();
            
        }
    }
}