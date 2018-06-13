using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.Test.Infrastructure.Common.Results
{
    public class Error
    {
        public Error(int code, string message)
        {
            Code = code;
            Message = message;
        }

        public int Code { get; }
        public string Message { get; }
    }
}