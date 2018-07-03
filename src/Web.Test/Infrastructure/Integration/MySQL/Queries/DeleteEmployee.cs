using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Dapper;
using Web.Test.Infrastructure.Common.Results;
using Web.Test.Infrastructure.Common.Results.Errors;
using Web.Test.Infrastructure.Domain.Contracts.Integration;
using Web.Test.Infrastructure.Integration.MySQL.Errors;

namespace Web.Test.Infrastructure.Integration.MySQL.Queries
{
    public class DeleteEmployee: VoidResult, IDbQuery
    {
        private readonly long employeeId;

        public DeleteEmployee(long employeeId)
        {
            this.employeeId = employeeId;
        }

        public void Execute(IDbContext context)
        {
            try
            {
                var dbConnection = context.GetConnection();            
                dbConnection.Execute("DELETE FROM Employees WHERE Id=@Id;", new {Id = employeeId }); 
            }
            catch(Exception ex)
            {
                base.AddErrors(new UnexpectedError(ex));
            }
        }
    }
}