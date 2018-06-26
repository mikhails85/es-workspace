using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Web.Test.Infrastructure.Domain.Contracts.Integration;

namespace Web.Test.Infrastructure.Integration.Elastic
{
    public abstract class ElasicStorage: IESStorage
    {
        private readonly ElasticSettings settings;
        
        private readonly IDictionary<Type, IESIndex<TEntity>> resolvers;
        
        public ElasicStorage(ElasticSettings settings)
        {
            this.settings = settings;
            this.resolvers = new Dictionary<Type, IESIndex<TEntity>>();
            this.OnInitEEntityResolvers();      
        }
        
        public virtual IESIndex<TEntity> Index => ResolveIndex<TEntity>();
        
        private IESIndex<TEntity> ResolveIndex<TEntity>()
        {
            var res = this.resolvers[typeof(TEntity)];
            res.Configure(this);
            return res;
        }
        
        protected abstract void OnInitEEntityResolvers();      
        
        protected virtual void Set<TEntity>(IESIndex<TEntity> resolver)
        {
            this.resolvers.Add(typeof(TEntity), resolver);
        }
    }
}