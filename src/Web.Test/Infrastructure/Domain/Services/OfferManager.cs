using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Web.Test.Infrastructure.Common.Results;
using Web.Test.Infrastructure.Common.Wrappers;
using Web.Test.Infrastructure.Domain.Contracts;
using Web.Test.Infrastructure.Domain.Contracts.Integration;
using Web.Test.Infrastructure.Domain.Models;
using Web.Test.Infrastructure.Integration.Elastic.Indexes.Queries;
using Web.Test.Infrastructure.Integration.MySQL.Queries;

namespace Web.Test.Infrastructure.Domain.Services
{
    public class OfferManager : IOfferManager
    {
        private readonly IDbContext context;
        private readonly IQueueManager queue;
        private readonly IESStorage esstorage;

        public OfferManager (IDbContext context, IQueueManager queue, IESStorage esstorage)
        {
             this.context = context;
             this.queue = queue;   
             this.esstorage = esstorage;
        }

        public VoidResult AddOffer(Offer offer)
        {
            var result = this.context.Query(new AddOffer(offer));
            if(result.Success)
            {                
                offer.Id = result.Value;    
                if(offer.RequaredSkills == null)          
                {
                    offer.RequaredSkills = new List<Skill>();
                }  
                foreach(var skill in offer.RequaredSkills)
                {
                   this.context.Query(new AddOfferSkill(offer.Id, skill.Id));     
                }

                context.Save();                
                queue.SendMessage("offers", new CRUDWrapper<Offer>{ Action = CRUDActionType.Create, Entity = offer });
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

            var delSkillResult = this.context.Query(new DeleteOfferSkills(id));
            if(!delSkillResult.Success)
            {
                return delSkillResult;
            }    

            var deleteResult = this.context.Query(new DeleteOffer(id));
            if(!deleteResult.Success)
            {
                return deleteResult;
            }

            this.context.Save();            
            queue.SendMessage("offers", new CRUDWrapper<Offer>{ Action = CRUDActionType.Delete, Entity = offer });

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
                queue.SendMessage("offers", new CRUDWrapper<Offer>{ Action = CRUDActionType.Update, Entity = offer });
            }            
            return updateResult;
        }

        public Result<Offer> GetOffer(long id)
        {
            return this.context.Query(new GetOffer(id));
        }

        public Result<IEnumerable<Offer>> GetList(int page, int size, string search)
        {
            return this.esstorage.Get<Offer>().Query(new SearchOffers(page, size, search));
        }
    }
}