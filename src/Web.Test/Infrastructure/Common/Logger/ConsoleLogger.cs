using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.Test.Infrastructure.Common.Logger
{
    public class ConsoleLogger:ILogger
    {
        public void LogError(string error)
        {            
            Console.ForegroundColor = ConsoleColor.Red;            
            Console.WriteLine($"ERROR ({DateTime.Now.ToString("dd/MM HH:mm:ss.fff")}):{error}"); 
            Console.ResetColor();
        }
        public void LogError(Exception error)
        {
            this.LogError(error);     
        }

        public void LogInfo(string info)
        {   
            Console.WriteLine($"INFO ({DateTime.Now.ToString("dd/MM HH:mm:ss.fff")}):{info}");            
        }
    }
}