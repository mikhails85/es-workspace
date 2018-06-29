using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNetCore.Mvc;
using Web.Test.Configuration;
using Web.Test.Infrastructure.Domain.Contracts;

namespace Web.Test.Controllers
{
    [Route("api/[controller]")]
    public class SkillsController : Controller
    {
        [HttpGet("[action]")]
        public IActionResult List(int page, int size, string search = null)
        {
            return this.Result(this.Service<ISkillManager>().GetList(page, size, search));
        }
    }
}