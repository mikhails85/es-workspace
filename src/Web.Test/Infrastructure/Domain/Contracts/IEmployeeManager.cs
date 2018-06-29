using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Web.Test.Infrastructure.Common.Results;
using Web.Test.Infrastructure.Domain.Models;

namespace Web.Test.Infrastructure.Domain.Contracts
{
    public interface IEmployeeManager
    {
        Result<IEnumerable<Employee>> GetList(int page, int size, string search);
        Result<Employee> GetProfile(string id);
        VoidResult AddProfile(Employee profile);
        VoidResult UpdateProfile(string id, Employee profile);
        VoidResult DeleteProfile(string id);
        VoidResult AddProject(string id, Project project);
        VoidResult DeleteProject(string id, string projectId);
    }
}