using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Contracts.CourseDetials
{
    public interface ICourseDetailsRepository : IRepositoryBase<CourseDetail>
    {
        Task<IEnumerable<CourseDetail>> GetAllCourseDetailsAsync(bool trackChanges);
        Task<CourseDetail> GetOneCourseDetailByIdAsync(int id, bool trackChanges);
    }
}
