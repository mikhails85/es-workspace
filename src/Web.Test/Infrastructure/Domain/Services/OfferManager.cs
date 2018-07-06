using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Web.Test.Infrastructure.Common.Results;
using Web.Test.Infrastructure.Domain.Contracts;
using Web.Test.Infrastructure.Domain.Contracts.Integration;
using Web.Test.Infrastructure.Domain.Models;
using Web.Test.Infrastructure.Integration.MySQL.Queries;

namespace Web.Test.Infrastructure.Domain.Services
{
    public class OfferManager : IOfferManager
    {
        private readonly IDbContext context;
        private readonly IQueueManager queue;

        public OfferManager (IDbContext context, IQueueManager queue)
        {
             this.context = context;
             this.queue = queue;   
        }

        public VoidResult AddOffer(Offer offer)
        {
            var result = this.context.Query(new AddOffer(offer));
            if(result.Success)
            {
                context.Save();
                offer.Id = result.Value;
                queue.SendMessage("insert.offer", offer);
            }            
            return result;
        }

        public VoidResult DeleteOffer(long id)
        {
             var result = this.GetOffer(id);            
            if(!result.Success)
            {
                return result;
            }

            var offer = result.Value;

            var deleteResult = this.context.Query(new DeleteOffer(id));
            if(!deleteResult.Success)
            {
                return deleteResult;
            }

            this.context.Save();
            queue.SendMessage("delete.offer", offer);

            return deleteResult;
        }

        public VoidResult UpdateOffer(Offer offer)
        {
             var result = this.GetOffer(offer.Id);            
            if(!result.Success)
            {
                return result;
            }

            var dbOffer = result.Value;
            dbOffer.Description = offer.Description;
            dbOffer.Name = offer.Name;
            
            var updateResult = this.context.Query(new UpdateOffer(dbOffer));
            if(updateResult.Success)
            {
                context.Save();
                queue.SendMessage("update.offer", offer);
            }            
            return updateResult;
        }

        public Result<Offer> GetOffer(long id)
        {
            return this.context.Query(new GetOffer(id));
        }

        public Result<IEnumerable<Offer>> GetList(int page, int size, string search)
        {
            throw new NotImplementedException();
        }
    }
}