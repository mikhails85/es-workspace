using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Web.Test.Infrastructure.Integration.Elastic.Indexes;

namespace Web.Test.Infrastructure.Integration.Elastic
{
    public class ESStorage : ElasicStorage
    {
        public ESStorage(ElasticSettings settings) 
                : base(settings)
        {
        }

        protected override void OnInitEEntityResolvers()
        {
            this.Set(x=>new EmployeeIndex(x));
        }
    }
}