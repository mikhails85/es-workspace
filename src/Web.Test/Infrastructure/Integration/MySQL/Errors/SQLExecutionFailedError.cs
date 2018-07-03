using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Web.Test.Infrastructure.Common.Results;

namespace Web.Test.Infrastructure.Integration.MySQL.Errors
{
    public class SQLExecutionFailedError : Error
    {
        public SQLExecutionFailedError(string message) 
            : base((int)ErrorCode.SQLExecutionFailed, message)
        {
        }
    }
}