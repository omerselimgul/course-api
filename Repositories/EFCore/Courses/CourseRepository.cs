using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Repositories.Contracts.Courses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.EFCore.Courses
{
    public class CourseRepository : RepositoryBase<Course>, ICourseRepository
    {
        public CourseRepository(RepositoryContext context) : base(context)
        {
        }
        public async Task<IEnumerable<Course>> GetAllCoursesAsync(bool trackChanges) =>
            await FindAll(trackChanges)
            .ToListAsync();

        public async Task<Course> GetOneCourseByIdAsync(int id, bool trackChanges)=>
            await FindByCondition(c=>c.Id.Equals(id),trackChanges)
            .SingleOrDefaultAsync();

        public void UpdateOneCourse(Course course)=> Update(course);
        public void CreateOneCourse(Course course) => Create(course);
        public void DeleteOneCourse(Course course) => Delete(course);
    }
}
