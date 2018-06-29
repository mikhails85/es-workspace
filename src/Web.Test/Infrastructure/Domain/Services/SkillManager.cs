using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Web.Test.Infrastructure.Common.Results;
using Web.Test.Infrastructure.Domain.Contracts;
using Web.Test.Infrastructure.Domain.Models;

namespace Web.Test.Infrastructure.Domain.Services
{
    public class SkillManager : ISkillManager
    {
        public Result<IEnumerable<Skill>> GetList(int page, int size, string search)
        {
            throw new NotImplementedException();
        }
    }
}