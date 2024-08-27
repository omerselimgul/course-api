using Services.Contracts.Applications;
using Services.Contracts.CourseDetails;
using Services.Contracts.Courses;
using Services.Contracts.Homeworks;
using Services.Contracts.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Contracts
{
    public interface IServiceManager
    {
        ICourseService CourseService { get; }
        IAuthenticationService AuthenticationService { get; }
        ICourseDetailService CourseDetailService { get; }
        IApplicationService ApplicationService { get; }
        IStudentHomeworkService StudentHomeworkService { get; }

    }
}
