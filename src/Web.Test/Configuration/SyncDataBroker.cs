using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNetCore.Builder;

namespace Web.Test.Configuration
{
    public static class SyncDataBroker
    {
        public static void UseSyncDataBroker (this IApplicationBuilder app)
        {
            var provider = app.ApplicationServices;    
            //TODO
        }
    }
}