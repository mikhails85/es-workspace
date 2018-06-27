using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Web.Test.Infrastructure.Common.Results;

namespace Web.Test.Configuration
{
    public static class ApiExtension
    {
        private static IServiceProvider provider;
        
        public static void UseApiExtension (this IApplicationBuilder app)
        {
            provider = app.ApplicationServices;    
        }
        
        public static T Service<T> (this Controller controller)
        {
            return provider.GetService<T>();
        }
        
        public static IActionResult Result(this Controller controller, VoidResult result)
        {
            var response = new ObjectResult(result);
            response.StatusCode = result.Success ? 200 : 500;
            return response;
        }
    }
}