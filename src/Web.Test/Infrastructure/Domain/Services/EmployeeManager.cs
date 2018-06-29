using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Web.Test.Infrastructure.Common.Results;
using Web.Test.Infrastructure.Domain.Contracts;
using Web.Test.Infrastructure.Domain.Models;

namespace Web.Test.Infrastructure.Domain.Services
{
    public class EmployeeManager : IEmployeeManager
    {
        public VoidResult AddProfile(Employee profile)
        {
            throw new NotImplementedException();
        }

        public VoidResult AddProject(string id, Project project)
        {
            throw new NotImplementedException();
        }

        public VoidResult DeleteProfile(string id)
        {
            throw new NotImplementedException();
        }

        public VoidResult DeleteProject(string id, string projectId)
        {
            throw new NotImplementedException();
        }

        public Result<IEnumerable<Employee>> GetList(int page, int size, string search)
        {
            throw new NotImplementedException();
        }

        public Result<Employee> GetProfile(string id)
        {
            throw new NotImplementedException();
        }

        public VoidResult UpdateProfile(string id, Employee profile)
        {
            throw new NotImplementedException();
        }
    }
}