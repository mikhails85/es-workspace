using System;
using System.Collections.Generic;
using System.Linq;
using Web.Test.Infrastructure.Domain.Contracts.Integration;
using System.Data.SqlClient;
using System.Data;
using Web.Test.Infrastructure.Domain.Models;
using Dapper;
using Web.Test.Infrastructure.Common.Results;
using Web.Test.Infrastructure.Common.Results.Errors;

namespace Web.Test.Infrastructure.Integration.MySQL.Queries
{
    public class GetAllSkills:Result<IEnumerable<Skill>>, IDbQuery
    {                        
        public void Execute(IDbContext context)
        {
            try
            {
                var dbConnection = context.GetConnection();            
                var result = dbConnection.Query<Skill>(
                                "SELECT * FROM Skills"
                            );            
                base.SetValue(result);             
            }
            catch(Exception ex)
            {
                base.AddErrors(new UnexpectedError(ex));
            }
        }
    }
}