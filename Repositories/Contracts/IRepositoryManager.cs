using Repositories.Contracts.Applications;
using Repositories.Contracts.CourseDetials;
using Repositories.Contracts.Courses;
using Repositories.Contracts.StudentHomeworks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Contracts
{
    public interface IRepositoryManager
    {
        ICourseRepository Course { get; }
        ICourseDetailsRepository CourseDetail { get; }
        IApplicationRepository Application { get; }
        IStudentHomeworkRepository StudentHomework { get; }


        Task SaveAsync();
    }
}
