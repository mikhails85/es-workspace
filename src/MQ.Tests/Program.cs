using System;
using System.Linq;
using System.Threading;
namespace MQ.Tests
{
    class Program
    {
        static void Main(string[] args)
        {
            var cmd = args.FirstOrDefault();
            if(!string.IsNullOrWhiteSpace(cmd) && cmd == "server")
            {
                Console.WriteLine("Server Run");   
                var server = new Server();
                for(var i=0; i<50; i++)
                {
                    server.SendMessage($"Message-{i}");    
                }                
            }
            
            if(!string.IsNullOrWhiteSpace(cmd) && cmd == "client")
            {
                Console.WriteLine($"Client Run {DateTime.Now.ToString()}");    
                var client = new Client();
                
                var nextDateStartDateTime = new DateTime(2018,6,5,14,53,0);
                double millisecondsToWait = (nextDateStartDateTime - DateTime.Now).TotalMilliseconds;
                
                var timer = new Timer(
                (o) => {
                    for(var i=0; i<5; i++)
                    {
                        client.GetMessage();                             
                    }                    
                },null,  (uint)millisecondsToWait, 0);                
            }
            
            Console.WriteLine(" Press [enter] to exit.");
            Console.ReadLine();
        }
    }
}