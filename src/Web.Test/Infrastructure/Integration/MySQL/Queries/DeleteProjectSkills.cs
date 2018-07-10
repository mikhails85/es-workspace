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
    public class DeleteProjectSkills: VoidResult, IDbQuery
    {        
        private readonly long projectId;

        public DeleteProjectSkills(long projectId)
        {
            this.projectId = projectId;            
        }

        public void Execute(IDbContext context)
        {
            try
            {
                var dbConnection = context.GetConnection();            
                var result = dbConnection.Execute("DELETE FROM ProjectsSkills WHERE ProjectId=@Id;", new {Id = projectId });                                                                      
            }
            catch(Exception ex)
            {
                base.AddErrors(new UnexpectedError(ex));
            }
        }
    }
}