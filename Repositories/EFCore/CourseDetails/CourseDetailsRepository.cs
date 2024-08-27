using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Repositories.Contracts.CourseDetials;
using Repositories.Contracts.Courses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.EFCore.CourseDetails
{
    public class CourseDetailsRepository : RepositoryBase<CourseDetail>, ICourseDetailsRepository
    {
        public CourseDetailsRepository(RepositoryContext context) : base(context)
        {

        }
        public async Task<IEnumerable<CourseDetail>> GetAllCourseDetailsAsync(bool trackChanges) =>
         await FindAll(trackChanges)
         .ToListAsync();

        public async Task<CourseDetail> GetOneCourseDetailByIdAsync(int id, bool trackChanges)
        {
            return await FindByCondition(c => c.Id.Equals(id), trackChanges)
            .SingleOrDefaultAsync();
        }
    }
}
