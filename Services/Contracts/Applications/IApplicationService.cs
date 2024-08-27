using Core.InputOutputModels.Applications;
using Core.InputOutputModels.Courses;
using Entities.DataTransferObjects.Courses;
using Entities.Models;
using Entities.RequestFeatures.Applications;
using Entities.RequestFeatures.Educators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Contracts.Applications
{
    public interface IApplicationService 
    {
        Task<ApplicationOutputModel> GetOneApplicationByIdAsync(int id, bool trackChanges);
        Task<ApplicationOutputModel> CreateOneApplicationAsync(ApplicationCreateModel createModel);
        Task UpdateStatusAsync(int id,byte status);


    }
}
