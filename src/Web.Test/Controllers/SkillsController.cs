using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNetCore.Mvc;
using Web.Test.Configuration;
using Web.Test.Infrastructure.Domain.Contracts;
using Web.Test.Infrastructure.Domain.Models;

namespace Web.Test.Controllers
{
    [Route("api/[controller]")]
    public class SkillsController : Controller
    {
        [HttpGet("[action]")]
        public IActionResult List()
        {
            return this.Result(this.Service<ISkillManager>().GetSkillList());
        }

        [HttpPost("[action]")]
        public IActionResult AddSkill([FromBody]Skill skill)
        {
            return this.Result(this.Service<ISkillManager>().AddSkill(skill));
        }
    }
}