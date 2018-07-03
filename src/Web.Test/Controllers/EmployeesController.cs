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
            return this.Result(this.Service<IEmployeeManager>().GetEmployeeList(page, size, search));
        }

        [HttpGet("{id}/[action]")]
        public IActionResult GetEmployee(long id)
        {
            return this.Result(this.Service<IEmployeeManager>().GetEmployee(id));
        }

        [HttpPost("[action]")]
        public IActionResult AddEmployee(Employee profile)
        {
            return this.Result(this.Service<IEmployeeManager>().AddEmployee(profile));
        }

        [HttpPut("{id}/[action]")]
        public IActionResult UpdateEmployee(Employee profile)
        {
            return this.Result(this.Service<IEmployeeManager>().UpdateEmployee(profile));
        }

        [HttpDelete("{id}/[action]")]
        public IActionResult DeleteEmployee(long id)
        {
            return this.Result(this.Service<IEmployeeManager>().DeleteEmployee(id));
        }

        [HttpPost("{id}/[action]")]
        public IActionResult AddProject(Project project) 
        {
            return this.Result(this.Service<IEmployeeManager>().AddProject(project));
        }

        [HttpDelete("{id}/[action]/{projectId}")]
        public IActionResult DeleteProject(long id, long projectId) 
        {
            return this.Result(this.Service<IEmployeeManager>().DeleteProject(id, projectId));
        }             
    }
}