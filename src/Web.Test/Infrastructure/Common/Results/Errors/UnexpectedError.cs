using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.Test.Infrastructure.Common.Results.Errors
{
    public class UnexpectedError: Error
    {
        public UnexpectedError(Exception ex)
            : base((int) ErrorCode.Unexpected, ex.ToString())
        {
        }
    }
}