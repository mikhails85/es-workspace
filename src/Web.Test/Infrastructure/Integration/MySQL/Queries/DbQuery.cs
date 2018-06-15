using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Web.Test.Infrastructure.Domain.Contracts.Integration;
using System.Data;
using System.Data.SqlClient;

namespace Web.Test.Infrastructure.Integration.MySQL.Queries
{
    public static class DbQuery
    {
        public static IDbTransaction GetTransaction(this IDbContext context)
        {           
            if(!(context is UnitOfWork))
            {
                throw new NotSupportedException("Context should be 'UnitOfWork' type"); 
            }
            
            return ((UnitOfWork)context).Transaction;
        }
        
        public static IDbConnection GetConnection(this IDbContext context)
        {
            return context.GetTransaction().Connection;
        }
    }
}