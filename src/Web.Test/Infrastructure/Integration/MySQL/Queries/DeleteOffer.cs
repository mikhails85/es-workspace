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
    public class DeleteOffer: VoidResult, IDbQuery
    {
        private readonly long offerId;

        public DeleteOffer(long offerId)
        {
            this.offerId = offerId;
        }

        public void Execute(IDbContext context)
        {
            try
            {
                var dbConnection = context.GetConnection();            
                dbConnection.Execute("DELETE FROM Offers WHERE Id=@Id;", new {Id = offerId }); 
            }
            catch(Exception ex)
            {
                base.AddErrors(new UnexpectedError(ex));
            }
        }
    }
}