using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Dapper;
using Web.Test.Infrastructure.Common.Results;
using Web.Test.Infrastructure.Common.Results.Errors;
using Web.Test.Infrastructure.Domain.Contracts.Integration;
using Web.Test.Infrastructure.Domain.Models;

namespace Web.Test.Infrastructure.Integration.MySQL.Queries
{
    public class GetOffer: Result<Offer>, IDbQuery
    {
        private readonly long offerId;
        private Dictionary<long, Offer> offers;
        public GetOffer(long offerId)
        {
            this.offerId = offerId;
        }

        public void Execute(IDbContext context)
        {
            try
            {
                var dbConnection = context.GetConnection();        
                
                offers = new Dictionary<long, Offer>();

                dbConnection.Query<Offer, Skill, Offer>(@"SELECT Offers.*, Skills.*
                            FROM Offers                            
                            LEFT JOIN OffersSkills ON Offers.Id = OffersSkills.OfferId
                            LEFT JOIN Skills ON OffersSkills.SkillId = Skills.Id
                            WHERE Offers.Id = @Id;", Mapping, new {Id = offerId });  

                base.SetValue(offers[offerId]);
            }
            catch(Exception ex)
            {
                base.AddErrors(new UnexpectedError(ex));
            }
        }

        private Offer Mapping(Offer offer, Skill skill)
        {
            Offer o;

            if (!offers.TryGetValue(offer.Id, out o))
            {             
                o = offer;
                o.RequaredSkills = new List<Skill>();
                offers.Add(o.Id,o);
            }
            
            if (o.RequaredSkills.All(s => s.Id != skill.Id))
            {
                o.RequaredSkills.Add(skill);
            }
            return o;
        }
    }
}