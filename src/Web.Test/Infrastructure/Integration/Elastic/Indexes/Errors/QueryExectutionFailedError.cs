using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Web.Test.Infrastructure.Common.Results;

namespace Web.Test.Infrastructure.Integration.Elastic.Indexes.Errors
{
    public class QueryExectutionFailedError: Error
    {
        public QueryExectutionFailedError(string message) 
            : base((int)ErrorCode.QueryExectutionFailed, message)
        {
        }
    }
}