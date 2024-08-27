using Entities.DataTransferObjects.Courses;
using Entities.Models;
using Core.InputOutputModels.Courses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Contracts.Courses
{
    public interface ICourseService 
    {
        Task<IEnumerable<CourseOutputModel>> GetAllCoursesAsync(bool trackChanges);
        Task<CourseOutputModel> GetOneCourseByIdAsync(int id, bool trackChanges);
        Task<Course> CreateOneCourseAsync(CourseInputModel model);
        Task UpdateOneCourseAsync(int id, CourseInputModel courseInputModel);
        Task DeleteOneCourseAsync(int id, bool trackChanges);
    }
}
