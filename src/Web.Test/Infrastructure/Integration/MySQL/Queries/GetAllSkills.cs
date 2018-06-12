using System;
using System.Collections.Generic;
using System.Linq;
using Web.Test.Infrastructure.Domain.Contracts.Integration;
using System.Data.SqlClient;
using System.Data;
using Web.Test.Infrastructure.Domain.Models;
using Dapper;

namespace Web.Test.Infrastructure.Integration.MySQL.Queries
{
    public class GetAllSkills:IDbQuery
    {
        public IEnumerable<Skill> Result {get; set;}
                        
        public void Execute(IDbContext context)
        {
            var dbTransaction = ((UnitOfWork)context).Transaction;
            var dbConnection = dbTransaction.Connection;
            
            this.Result = dbConnection.Query<Skill>(
                                "SELECT * FROM Skills"
                            );         
        }
    }
}