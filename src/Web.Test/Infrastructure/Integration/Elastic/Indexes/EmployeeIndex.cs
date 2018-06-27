using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Nest;
using Web.Test.Infrastructure.Domain.Contracts.Integration;
using Web.Test.Infrastructure.Domain.Models;

namespace Web.Test.Infrastructure.Integration.Elastic.Indexes
{
    public class EmployeeIndex :  ESIndex<Employee>
    {
        private const string Index ="Employees";
        public EmployeeIndex(ElasicStorage storage)        
        :base(storage, Index)
        {            
        }
        protected override void CreateIndex()
        {
            throw new NotImplementedException();
        }
    }
}