using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Dapper;
using Web.Test.Infrastructure.Common.Results;
using Web.Test.Infrastructure.Common.Results.Errors;
using Web.Test.Infrastructure.Domain.Contracts.Integration;
using Web.Test.Infrastructure.Domain.Models;
using Web.Test.Infrastructure.Integration.MySQL.Errors;

namespace Web.Test.Infrastructure.Integration.MySQL.Queries
{
    public class UpdateOffer: VoidResult, IDbQuery
    {
        private readonly Offer offer;

        public UpdateOffer(Offer offer)
        {
            this.offer = offer;
        }

        public void Execute(IDbContext context)
        {
            try
            {
                var dbConnection = context.GetConnection();            
                var result = dbConnection.Execute("UPDATE Offers SET Name = @Name, Description= @Description WHERE Id = @Id;", offer);                                                     
                if(result <1)
                {
                    base.AddErrors(new SQLExecutionFailedError("UPDATE Offers"));    
                }
            }
            catch(Exception ex)
            {
                base.AddErrors(new UnexpectedError(ex));
            }
        }
    }
}