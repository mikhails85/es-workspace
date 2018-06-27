using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.Test.Infrastructure.Domain.Contracts.Integration
{
    public interface IESQuery<TEntity>
    {
        void Execute(IESIndex<TEntity> context);
    }
}