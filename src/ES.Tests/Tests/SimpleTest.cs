using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ES.Tests.Models;
namespace ES.Tests.Tests
{
    public class SimpleTest:ESStorage
    {
        public SimpleTest()
        :base("simple")
        {}
        
        public void RunCreateIndexTest()
        {
            Console.WriteLine($"Try create index:'{this.Index}'");
            CreateIndex();
            
            Console.WriteLine($"Index created:'{this.IndexExists()}'");
            
            Console.WriteLine($"Try remove index:'{this.Index}'");
            this.RemoveIndex();
            
            Console.WriteLine($"Index removed:'{!this.IndexExists()}'");            
        }
        
        private void CreateIndex()
        {
            var createIndexResponse = this.Client.CreateIndex(this.Index, c => c
                .Settings(s => s
                    .NumberOfShards(1)
                    .NumberOfReplicas(0)
                )
                .Mappings(m => m
                    /*.Map<Employee>(mm => mm
                        .AutoMap()
                    )
                    .Map<Offer>(mm => mm
                        .AutoMap()
                    )*/
                    .Map<OfferQuery>(mm => mm
                        .AutoMap()
                        .Properties(p => p                            
                            .Percolator(pp => pp
                                .Name(n => n.Query)
                            )
                        )
                    )
                )
            );
            
            if (!createIndexResponse.IsValid)
                Console.WriteLine($"Error: {createIndexResponse.DebugInformation}");
        }
    }
}