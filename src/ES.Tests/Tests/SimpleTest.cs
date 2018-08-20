using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ES.Tests.Models;
using Nest;
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
            CreateIndex<Employee>();
            
            Console.WriteLine($"Index created:'{this.IndexExists()}'");
            
            Console.WriteLine($"Try remove index:'{this.Index}'");
            this.RemoveIndex();
            
            Console.WriteLine($"Index removed:'{!this.IndexExists()}'");            
        }
        
        public void RunAddToIndexTest()
        {   
            Console.WriteLine($"Try create index:'{this.Index}'");
            CreatePercolatorIndex();            
            Console.WriteLine($"Index created:'{this.IndexExists(this.Index)}'");
            
            Console.WriteLine($"Try add to index document");
            var result = this.AddToIndex(new OfferQuery{Id="1", Query = new MatchQuery 
                {
                    Field = Infer.Field<Employee>(entry => entry.Skills),
                    Query = "skill"
                }}, this.Index);
            
             if (!result.IsValid)
                Console.WriteLine($"Error: {result.DebugInformation}");
                        
            Console.WriteLine($"Try remove index:'{this.Index}'");
            this.RemoveIndex(this.Index);            
            Console.WriteLine($"Index removed:'{!this.IndexExists(this.Index)}'");            
        }
        
        public void ReversSearchingTest()
        {   
            Console.WriteLine($"Try create index:'{this.Index}'");
            CreatePercolatorIndex(this.Index);            
            Console.WriteLine($"Index created:'{this.IndexExists(this.Index)}'");
            
            Console.WriteLine($"Try add to index document");
            var result = this.AddToIndex(new OfferQuery{Id="1", Query = new MatchQuery 
                {
                    Field = Infer.Field<Employee>(entry => entry.Skills),
                    Query = "skill"
                }}, this.Index);
            
             if (!result.IsValid)
                Console.WriteLine($"Error: {result.DebugInformation}");
            
            Console.WriteLine($"Try search");
            var search = Search(new Employee{Skills = new List<string>{"skill"}});
            if (!search.IsValid)
                Console.WriteLine($"Error: {result.DebugInformation}");            
                
            foreach(var doc in search.Documents)         
            {
                Console.WriteLine($"Doc Id: {doc.Id}");
            }
            
            Console.WriteLine($"Try search");
            search = Search(new Employee{Skills = new List<string>{"skin"}});
            if (!search.IsValid)
                Console.WriteLine($"Error: {result.DebugInformation}");            
                
            foreach(var doc in search.Documents)         
            {
                Console.WriteLine($"Doc Id: {doc.Id}");
            }
             
            Console.WriteLine($"Try remove index:'{this.Index}'");
            this.RemoveIndex(this.Index);            
            Console.WriteLine($"Index removed:'{!this.IndexExists(this.Index)}'");            
        }
        
        public void AggregationsTest()
        {   
            Console.WriteLine($"Try create index:'{this.Index}'");
            CreateIndex<Offer>(this.Index);            
            Console.WriteLine($"Index created:'{this.IndexExists(this.Index)}'");
            
            for(var i=1; i<=10; i++)
            {
                Console.WriteLine($"Try add to index document {i}");
                var result = this.AddToIndex(
                                    new Offer{ Index =i, Title = $"Offer {i}", Description = $"Offer {i} Description", 
                                        Skills= new List<Skill>{
                                                new Skill{Id=1, Name = "C#" }, 
                                                new Skill{Id=2, Name = "Js" }, 
                                                new Skill{Id=3, Name = "Vue.js"}, 
                                                new Skill{Id=4, Name = "MySql"} 
                                                }
                                        }, this.Index);
            
                if (!result.IsValid)
                    Console.WriteLine($"Error: {result.DebugInformation}");                
            }
            
            Console.WriteLine($"Try Aggregat");
            var aggregations = this.Client.Search<Offer>(s=>s
                                                            .Aggregations(a => a
                                                            .Terms("skills", ta => ta
                                                                .Field(f=>f.Skills.First().Id)
                                                            )
                                                        ));
            if (!aggregations.IsValid)
                Console.WriteLine($"Error: {aggregations.DebugInformation}");            
                
            foreach(var bucket in aggregations.Aggregations.Terms("skills").Buckets)         
            {
               Console.WriteLine($"key: {bucket.Key}, count: {bucket.DocCount}");
            }
                         
            Console.WriteLine($"Try remove index:'{this.Index}'");
            this.RemoveIndex(this.Index);            
            Console.WriteLine($"Index removed:'{!this.IndexExists(this.Index)}'");            
        }
        
        private void CreatePercolatorIndex(string index = null)
        {
            var createIndexResponse = this.Client.CreateIndex(index ?? this.Index, c => c
                .Settings(s => s
                    .NumberOfShards(1)
                    .NumberOfReplicas(0)
                )
                .Mappings(m => m                    
                    .Map<OfferQuery>(mm => mm
                        .AutoMap()
                        .Properties(p => p                            
                            .Percolator(pp => pp
                                .Name(n => n.Query)
                            )
                        ).AutoMap<Employee>()
                    )
                )
            );
            
            if (!createIndexResponse.IsValid)
                Console.WriteLine($"Error: {createIndexResponse.DebugInformation}");               
           
        }
        
        private void CreateIndex<T>(string index = null) where T:class
        {
            var createIndexResponse = this.Client.CreateIndex(index ?? this.Index, c => c
                .Settings(s => s
                    .NumberOfShards(1)
                    .NumberOfReplicas(0)
                )
                .Mappings(m => m                    
                    .Map<T>(mm => mm
                        .AutoMap()
                    )
                )
            );
            
            if (!createIndexResponse.IsValid)
                Console.WriteLine($"Error: {createIndexResponse.DebugInformation}");               
           
        }
        
        private ISearchResponse<OfferQuery> Search(Employee employee)
        {
             var searchResponse = this.Client.Search<OfferQuery>(s => s
                .Query(q => q
                    .Percolate(p => p
                        .Field(f => f.Query)
                        .Document(employee)
                    )
                )
            );
            
            return searchResponse;
        }
    }
}