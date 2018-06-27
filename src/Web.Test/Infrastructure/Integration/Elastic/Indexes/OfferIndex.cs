using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Web.Test.Infrastructure.Domain.Contracts.Integration;
using Web.Test.Infrastructure.Domain.Models;

namespace Web.Test.Infrastructure.Integration.Elastic.Indexes
{
    public class OfferIndex : ESIndex<Offer>
    {
        private const string Index ="Offers";

        public OfferIndex(ElasicStorage storage) 
            : base(storage, Index)
        {
        }

        protected override void CreateIndex()
        {
            throw new NotImplementedException();
        }
    }
}