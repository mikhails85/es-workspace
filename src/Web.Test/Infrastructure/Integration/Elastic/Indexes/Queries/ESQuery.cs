using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Nest;
using Web.Test.Infrastructure.Domain.Contracts.Integration;

namespace Web.Test.Infrastructure.Integration.Elastic.Indexes.Queries
{
    public static class ESQuery
    {
        public static ElasticClient GetClient<TEntity>(this IESIndex<TEntity> index)
        {
            return ((ESIndex<TEntity>)index).Client;
        }
    }
}