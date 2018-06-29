using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNetCore.Mvc;
using Web.Test.Infrastructure.Domain.Contracts;
using Web.Test.Infrastructure.Domain.Models;
using Web.Test.Configuration;

namespace Web.Test.Controllers
{
    [Route("api/[controller]")]
    public class EmployeesController : Controller
    {
        [HttpGet("[action]")]
        public IActionResult List(int page, int size, string search = null)
        {
            return this.Result(this.Service<IEmployeeManager>().GetList(page, size, search));
        }

        [HttpGet("{id}/[action]")]
        public IActionResult GetProfile(string id)
        {
            return this.Result(this.Service<IEmployeeManager>().GetProfile(id));
        }

        [HttpPost("[action]")]
        public IActionResult AddProfile(Employee profile)
        {
            return this.Result(this.Service<IEmployeeManager>().AddProfile(profile));
        }

        [HttpPut("{id}/[action]")]
        public IActionResult UpdateProfile(string id, Employee profile)
        {
            return this.Result(this.Service<IEmployeeManager>().UpdateProfile(id, profile));
        }

        [HttpDelete("{id}/[action]")]
        public IActionResult DeleteProfile(string id)
        {
            return this.Result(this.Service<IEmployeeManager>().DeleteProfile(id));
        }

        [HttpPost("{id}/[action]")]
        public IActionResult AddProject(string id, Project project) 
        {
            return this.Result(this.Service<IEmployeeManager>().AddProject(id, project));
        }

        [HttpDelete("{id}/[action]/{projectId}")]
        public IActionResult DeleteProject(string id, string projectId) 
        {
            return this.Result(this.Service<IEmployeeManager>().DeleteProject(id, projectId));
        }             
    }
}