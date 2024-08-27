using Entities.Models;
using Entities.RequestFeatures.Applications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Contracts.Applications
{
    public interface IApplicationRepository : IRepositoryBase<Application>
    {
        void CreateOneApplication(Application application);
        Task<Application> GetOneApplicationByIdAsync(int id, bool trackChanges);

    }
}
