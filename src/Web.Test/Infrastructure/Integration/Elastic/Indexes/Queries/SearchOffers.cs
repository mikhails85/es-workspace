using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Web.Test.Infrastructure.Common.Results;
using Web.Test.Infrastructure.Domain.Contracts.Integration;
using Web.Test.Infrastructure.Domain.Models;
using Web.Test.Infrastructure.Integration.Elastic.Indexes.Errors;
using Web.Test.Infrastructure.Integration.Elastic.Indexes.Models;

namespace Web.Test.Infrastructure.Integration.Elastic.Indexes.Queries
{
    public class SearchOffers: Result<IEnumerable<Offer>>, IESQuery<Offer>
    {
        private readonly int page;

        public int size { get; }

        private readonly string search;

        public SearchOffers(int page, int size, string search)
        {
            this.page = page;
            this.size = size;
            this.search = search;
        }
        public void Execute(IESIndex<Offer> context)
        {
            var client = context.GetClient();
            var searchResponse = client.Search<ESOffer>(s => s
                .From(page)
                .Size(size)
                .Query(q => q
                     .MultiMatch(mp => mp.Fields(f => f.Fields(e => e.Id, e => e.Name)).Query(search))
                )
            );

            if (!searchResponse.IsValid)
            {
                this.AddErrors(new QueryExectutionFailedError(searchResponse.DebugInformation));
                return;    
            }
                
            var employees = new List<Offer>();   
            foreach(var doc in searchResponse.Documents)         
            {
                employees.Add(doc);
            }

            SetValue(employees);
        }
    }
}