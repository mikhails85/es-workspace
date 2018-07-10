using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Dapper;
using Web.Test.Infrastructure.Common.Results;
using Web.Test.Infrastructure.Common.Results.Errors;
using Web.Test.Infrastructure.Domain.Contracts.Integration;

namespace Web.Test.Infrastructure.Integration.MySQL.Queries
{
    public class AddProjectSkill: Result<long>, IDbQuery
    {
        private readonly long skillId;
        private readonly long projectId;

        public AddProjectSkill(long projectId, long skillId)
        {
            this.projectId = projectId;
            this.skillId = skillId;
        }

        public void Execute(IDbContext context)
        {
            try
            {
                var dbConnection = context.GetConnection();            
                var result = dbConnection.Query<long>(@"INSERT INTO ProjectsSkills (ProjectId, SkillId) VALUES (@ProjectId, @SkillId);
                                                          SELECT CAST(LAST_INSERT_ID() AS UNSIGNED INTEGER);", new{ProjectId=projectId, SkillId = skillId});                                                     
                SetValue(result.FirstOrDefault());
            }
            catch(Exception ex)
            {
                base.AddErrors(new UnexpectedError(ex));
            }
        }
    }
}