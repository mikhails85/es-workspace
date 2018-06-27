using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.Test.Infrastructure.Common.Logger
{
    public interface ILogger
    {
        void LogError(string error);
        void LogError(Exception error);
        void LogInfo(string info);
    }
}