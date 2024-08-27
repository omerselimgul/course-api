using Entities.Exceptions;
using Entities.Models;
using Entities.RequestFeatures.Applications;
using Microsoft.EntityFrameworkCore;
using Repositories.Contracts.Applications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.EFCore.Applications
{
    public class ApplicationRepository : RepositoryBase<Application>, IApplicationRepository
    {
        public ApplicationRepository(RepositoryContext context) : base(context)
        {
        }
        public void CreateOneApplication(Application application)=>Create(application);

        public async Task<Application> GetOneApplicationByIdAsync(int id, bool trackChanges) =>
            await FindByCondition(c => c.ApplicationId.Equals(id), trackChanges)
            .SingleOrDefaultAsync();
    }
}
