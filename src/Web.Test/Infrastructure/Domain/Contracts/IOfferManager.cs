using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Web.Test.Infrastructure.Common.Results;
using Web.Test.Infrastructure.Domain.Models;

namespace Web.Test.Infrastructure.Domain.Contracts
{
    public interface IOfferManager
    {
        Result<IEnumerable<Offer>> GetList(int page, int size, string search);
        Result<Offer> GetOffer(long id);
        VoidResult UpdateOffer(Offer offer);
        VoidResult DeleteOffer(long id);
        VoidResult AddOffer(Offer offer);
    }
}