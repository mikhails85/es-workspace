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
    public class AddProject : VoidResult, IDbQuery
    {
        private readonly Project project;

        public AddProject(Project project)
        {
            this.project = project;
        }

        public void Execute(IDbContext context)
        {
            try
            {
                var dbConnection = context.GetConnection();            
                var result = dbConnection.Execute("INSERT INTO Projects (EmployeeId, Name, Description) VALUES (@EmployeeId, @Name, @Description);", project);                                                     
                if(result <1)
                {
                    base.AddErrors(new SQLExecutionFailedError("INSERT INTO Projects"));    
                }
            }
            catch(Exception ex)
            {
                base.AddErrors(new UnexpectedError(ex));
            }
        }
    }
}