using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Web.Test.Infrastructure.Common.Results;
using Web.Test.Infrastructure.Domain.Contracts;
using Web.Test.Infrastructure.Domain.Models;

namespace Web.Test.Infrastructure.Domain.Services
{
    public class OfferManager : IOfferManager
    {
        public VoidResult AddOffer(Offer offer)
        {
            throw new NotImplementedException();
        }

        public VoidResult DeleteOffer(long id)
        {
            throw new NotImplementedException();
        }

        public Result<IEnumerable<Offer>> GetList(int page, int size, string search)
        {
            throw new NotImplementedException();
        }

        public Result<Offer> GetOffer(long id)
        {
            throw new NotImplementedException();
        }

        public VoidResult UpdateOffer(Offer offer)
        {
            throw new NotImplementedException();
        }
    }
}