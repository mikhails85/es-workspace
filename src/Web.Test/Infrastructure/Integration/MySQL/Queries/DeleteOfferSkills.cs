using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Dapper;
using Web.Test.Infrastructure.Common.Results;
using Web.Test.Infrastructure.Common.Results.Errors;
using Web.Test.Infrastructure.Domain.Contracts.Integration;

namespace Web.Test.Infrastructure.Integration.MySQL.Queries
{
    public class DeleteOfferSkills: VoidResult, IDbQuery
    {        
        private readonly long offerId;

        public DeleteOfferSkills(long offerId)
        {
            this.offerId = offerId;            
        }

        public void Execute(IDbContext context)
        {
            try
            {
                var dbConnection = context.GetConnection();            
                var result = dbConnection.Execute("DELETE FROM OffersSkills WHERE OfferId=@Id;", new {Id = offerId });                                                                      
            }
            catch(Exception ex)
            {
                base.AddErrors(new UnexpectedError(ex));
            }
        }
    }
}