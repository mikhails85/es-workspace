using System;
using System.Collections.Generic;
using System.Linq;
using Nest;
namespace ES.Tests
{
    public abstract class ESStorage
    {
        protected string Index {get;}
        protected ElasticClient Client{get;}
        
        protected ESStorage(string index)
        {
            this.Index = index;      
            var node = new Uri("http://localhost:9200");
            var settings = new ConnectionSettings(node).DefaultIndex(this.Index);
            this.Client = new ElasticClient(settings);
        }
        
        protected void RemoveIndex(string index = null) 
        {
             this.Client.DeleteIndex(index ?? this.Index);
        }
        
        protected bool IndexExists(string index = null) 
        {
            return this.Client.IndexExists(index ?? this.Index).Exists;
        }                
        
        protected void PutDocument<T>(T doc) where T:class
        {
            this.Client.IndexDocument(doc);
        }
        
        protected void RemoveDocument<T>(string id) where T:class
        {
            this.Client.Delete<T>(id);
        }
        
        protected IIndexResponse AddToIndex<T>(T doc, string index = null) where T:class
        {
            return this.Client.Index(doc, d => d.Index(index ?? this.Index));    
        }        
    }
}