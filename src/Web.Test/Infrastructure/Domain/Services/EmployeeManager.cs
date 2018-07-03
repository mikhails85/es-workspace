using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Web.Test.Infrastructure.Common.Results;
using Web.Test.Infrastructure.Domain.Contracts;
using Web.Test.Infrastructure.Domain.Contracts.Integration;
using Web.Test.Infrastructure.Domain.Models;
using Web.Test.Infrastructure.Integration.MySQL.Queries;

namespace Web.Test.Infrastructure.Domain.Services
{
    public class EmployeeManager : IEmployeeManager
    {
        private readonly IDbContext context;
        private readonly IQueueManager queue;

        public EmployeeManager (IDbContext context, IQueueManager queue)
        {
             this.context = context;
             this.queue = queue;   
        }

        public VoidResult AddEmployee(Employee profile)
        {
            var result = this.context.Query(new AddEmployee(profile));
            if(result.Success)
            {
                context.Save();
                profile.Id = result.Value;
                queue.SendMessage("insert.employee", profile);
            }            
            return result;
        }

        public VoidResult AddProject(Project project)
        {
            var result = this.GetEmployee(project.Id);            
            if(!result.Success)
            {
                return result;
            }

            var profile = result.Value;

            var projectResult = this.context.Query(new AddProject(project));
            if(projectResult.Success)
            {
                context.Save();
                profile.Projects.Add(project);
                queue.SendMessage("update.employee", profile);
            }           

            return projectResult;            
        }

        public VoidResult DeleteEmployee(long id)
        {
            var result = this.GetEmployee(id);            
            if(!result.Success)
            {
                return result;
            }

            var profile = result.Value;

            var delProjResult = this.context.Query(new DeleteEmployeeProjects(id));
            if(!delProjResult.Success)
            {
                return delProjResult;
            }

            var delEmplResult = this.context.Query(new DeleteEmployee(id));
            if(!delEmplResult.Success)
            {
                return delEmplResult;
            }

            this.context.Save();
            queue.SendMessage("delete.employee", profile);

            return delEmplResult;
        }

        public VoidResult DeleteProject(long id, long projectId)
        {
            var result = this.GetEmployee(id);            
            if(!result.Success)
            {
                return result;
            }

            var profile = result.Value;

            var delProjResult = this.context.Query(new DeleteProject(projectId));
            if(!delProjResult.Success)
            {
                return delProjResult;
            }

            this.context.Save();

            profile.Projects = profile.Projects.Where(x=>x.Id != projectId).ToList(); 
            queue.SendMessage("update.employee", profile);

            return delProjResult;
        }

        public VoidResult UpdateEmployee(Employee employee)
        {
            var result = this.GetEmployee(employee.Id);            
            if(!result.Success)
            {
                return result;
            }

            var profile = result.Value;
            profile.JobTitle = employee.JobTitle;
            profile.Name = employee.Name;
            profile.Photo = employee.Photo;
            profile.CV = employee.CV;
            
            var updateResult = this.context.Query(new UpdateEmployee(profile));
            if(updateResult.Success)
            {
                context.Save();
                queue.SendMessage("update.employee", profile);
            }            
            return updateResult;
        }

        public Result<Employee> GetEmployee(long id)
        {
            return this.context.Query(new GetEmployee(id));
        }
        
        public Result<IEnumerable<Employee>> GetEmployeeList(int page, int size, string search)
        {
            throw new NotImplementedException();
        }
        
    }
}