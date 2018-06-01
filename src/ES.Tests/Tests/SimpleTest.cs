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