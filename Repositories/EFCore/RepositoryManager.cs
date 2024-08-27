using Repositories.Contracts;
using Repositories.Contracts.Applications;
using Repositories.Contracts.CourseDetials;
using Repositories.Contracts.Courses;

using Repositories.Contracts.StudentHomeworks;
using Repositories.EFCore.Applications;
using Repositories.EFCore.CourseDetails;
using Repositories.EFCore.Courses;
using Repositories.EFCore.StudentHomeworks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.EFCore
{
    public class RepositoryManager : IRepositoryManager
    {
        private readonly RepositoryContext _context;
        private readonly Lazy<ICourseRepository> _courseRepository;
        private readonly Lazy<ICourseDetailsRepository> _courseDetailsRepository;
        private readonly Lazy<IApplicationRepository> _applicationRepository;
        private readonly Lazy<IStudentHomeworkRepository> _studenthomeworkRepository;




        public RepositoryManager(RepositoryContext context)
        {
            _context = context;
            _courseRepository = new Lazy<ICourseRepository>(() => new CourseRepository(_context));
            _courseDetailsRepository = new Lazy<ICourseDetailsRepository>(() => new CourseDetailsRepository(_context));
            _applicationRepository = new Lazy<IApplicationRepository>(()=> new ApplicationRepository(_context));
            _studenthomeworkRepository = new Lazy<IStudentHomeworkRepository>(() => new StudentHomeworkRepository(_context));


        }

        public ICourseRepository Course => _courseRepository.Value;

        public ICourseDetailsRepository CourseDetail => _courseDetailsRepository.Value;
        public IApplicationRepository Application => _applicationRepository.Value;
        public IStudentHomeworkRepository StudentHomework => _studenthomeworkRepository.Value;


        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
