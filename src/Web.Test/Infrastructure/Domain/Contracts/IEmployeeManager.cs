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
        VoidResult AddEmployee(Employee profile);
        VoidResult AddProject(Project project);
        VoidResult DeleteEmployee(long id);
        VoidResult DeleteProject(long id, long projectId);
        VoidResult UpdateEmployee(Employee employee);
        Result<Employee> GetEmployee(long id);
        Result<IEnumerable<Employee>> GetEmployeeList(int page, int size, string search);
    }
}