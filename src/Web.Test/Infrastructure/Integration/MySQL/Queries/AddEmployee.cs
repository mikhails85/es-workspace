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
    public class AddEmployee : Result<long>, IDbQuery
    {
        private readonly Employee employee;

        public AddEmployee(Employee employee)
        {
            this.employee = employee;
        }

        public void Execute(IDbContext context)
        {
            try
            {
                var dbConnection = context.GetConnection();            
                var result = dbConnection.Query<long>(@"INSERT INTO Employees (Name, JobTitle, Photo, CV) VALUES (@Name, @JobTitle, @Photo, @CV);
                                                          SELECT CAST(LAST_INSERT_ID() AS UNSIGNED INTEGER);", employee);                                                     
                SetValue(result.FirstOrDefault());
            }
            catch(Exception ex)
            {
                base.AddErrors(new UnexpectedError(ex));
            }
        }
    }
}