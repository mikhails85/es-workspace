using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.Test.Infrastructure.Domain.Contracts.Integration
{
    public interface IESIndex<TEntity>
    {
        TQuery Query<TQuery>(TQuery query) where TQuery:IESQuery<TEntity>;
    }
}