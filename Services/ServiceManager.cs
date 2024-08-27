using AutoMapper;
using Entities.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Repositories.Contracts;
using Repositories.EFCore;
using Services.Contracts;
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

namespace Services
{
    public class ServiceManager : IServiceManager
    {
        private readonly Lazy<ICourseService> _courseService;
        private readonly Lazy<IAuthenticationService> _authenticationService;
        private readonly Lazy<ICourseDetailService> _courseDetailService;
        private readonly Lazy<IApplicationService> _applicationService;
        private readonly Lazy<IStudentHomeworkService> _studdenthomeworkService;



        public ServiceManager(
            IRepositoryManager repositoryManager,
            ILoggerService logger,
            IMapper mapper,
            IConfiguration configuration,
            UserManager<User> userManager,
            RepositoryContext _context
            )
        {
           _courseService = new Lazy<ICourseService>(() => 
           new CourseManager(repositoryManager,logger,mapper, _context));
            
            _authenticationService = new Lazy<IAuthenticationService>(() =>
            new AuthenticationManager(logger,mapper,userManager,configuration));

            _courseDetailService = new Lazy<ICourseDetailService>(() =>
             new CourseDetailManager(repositoryManager, logger, mapper));

            _applicationService = new Lazy<IApplicationService>(() =>
            new ApplicationManager(userManager,repositoryManager, logger, mapper,_context));

            _studdenthomeworkService = new Lazy<IStudentHomeworkService>(() =>
           new StudentHomeworkManager(repositoryManager, logger, mapper));
           
        

        }
        public ICourseService CourseService => _courseService.Value;

		public IAuthenticationService AuthenticationService => _authenticationService.Value;


        public ICourseDetailService CourseDetailService => _courseDetailService.Value;

        public IApplicationService ApplicationService => _applicationService.Value;
       
        public IStudentHomeworkService StudentHomeworkService => _studdenthomeworkService.Value;

    }
}
