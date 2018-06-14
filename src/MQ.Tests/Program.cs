using System;
using System.Linq;
using System.Threading;
using System.Collections.Generic;

namespace MQ.Tests
{
    class Program
    {
        static void Main(string[] args)
        {
            var cmd = args.FirstOrDefault();
            if(!string.IsNullOrWhiteSpace(cmd) && cmd == "server")
            {
                Console.WriteLine($"Server Run {DateTime.Now.ToString()}");   
                var server = new Server();
                for(var i=0; i<500; i++)
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
            
            if(!string.IsNullOrWhiteSpace(cmd) && cmd == "event")
            {
                Console.WriteLine($"Event Client Run {DateTime.Now.ToString()}");    
                var batch = new Batch();
                batch.Handler +=  WiriteBatch;               
                
                var client = new EventClient(new ClientSettings{Host="localhost", PrefetchCount=10});
                
                 var nextDateStartDateTime = new DateTime(2018,6,14,11,53,0);
                double millisecondsToWait = (nextDateStartDateTime - DateTime.Now).TotalMilliseconds;
                
                //client.Start(batch.Name, batch.Add);
                
                var timer = new Timer(
                (o) => {
                     Console.WriteLine("Run Lisener");
                     client.Start(batch.Name, batch.Add);                  
                },null,  (uint)millisecondsToWait, 0);   
            }
            
            Console.WriteLine(" Press [enter] to exit.");
            Console.ReadLine();
        }
               
        public static void WiriteBatch(List<string> batch)       
        {
            Console.WriteLine($"New Batch {DateTime.Now.ToString()}");   
            foreach(var msg in batch)
            {
                 Console.WriteLine(msg);
            }
        }
    }
}