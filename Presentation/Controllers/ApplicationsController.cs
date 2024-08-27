using Core.InputOutputModels.Applications;
using Core.Utilities.Results;
using Entities.Models;
using Entities.RequestFeatures.Applications;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Repositories.EFCore;
using Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;


namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/application")]
    public class ApplicationsController : ControllerBase
    {
        private readonly IServiceManager _manager;
        protected readonly RepositoryContext _context;
        private readonly UserManager<User> _userManager;

        public ApplicationsController(IServiceManager manager,RepositoryContext context, UserManager<User> userManager)
        {
            _manager = manager;
            _context = context;
            _userManager = userManager;
        }



        [HttpGet("ByUserId")]
        public async Task<IDataResult<List<ApplicationOutputModel>>> GetApplicationsByUserIdAsync([FromQuery] string userId, [FromQuery] byte? status)
        {
            var userInfo =await  _context.Set<User>().FirstOrDefaultAsync(x=>x.Id.Equals(userId));

         
            var roles = await _userManager.GetRolesAsync(userInfo);
            var rolesList = roles.ToList();

            if (rolesList.Any(x=>x.Equals("Admin")))
            {
                return await GetApplicationsForAdmin(status);

            }
            else if (rolesList.Any(x => x.Equals("Student")))
            {

                return await GetApplicationsForStudentAsync(userId,status);

            }
            else if (rolesList.Any(x => x.Equals("Educator")))
            {

                return await GetApplicationsForEducatorAsync(userId,status);

            }
            return new ErrorDataResult<List<ApplicationOutputModel>>("Kullanici bulunamadi.");
        }


        [HttpGet]
        public async Task<IDataResult<List<ApplicationOutputModel>>> GetAllApplicationsAsync([FromQuery] ApplicationParameters applicationParameters)
        {
            IQueryable<ApplicationOutputModel> query = from course in _context.Courses
                                                       join application in _context.Applications on course.Id equals application.CourseId
                                                       join user in _context.Users on application.UserId equals user.Id
                                                       select new ApplicationOutputModel
                                                       {
                                                           ApplicationId = application.ApplicationId,
                                                           CourseId = course.Id,
                                                           UserId = user.Id,
                                                           UserName = user.UserName,
                                                           FirstName = user.FirstName,
                                                           LastName = user.LastName,
                                                           Gender = user.Gender,
                                                           BirthDate = user.BirthDate,
                                                           City = user.City,
                                                           District = user.District,
                                                           Address = user.Address,
                                                           HighSchool = user.HighSchool,
                                                           HighSchoolGradiationDate = user.HighSchoolGradiationDate,
                                                           HighSchoolFile = user.HighSchoolFile,
                                                           University = user.University,
                                                           UniversityDepartment = user.UniversityDepartment,
                                                           UniversityGradiationDate = user.UniversityGradiationDate,
                                                           UniversityFile = user.UniversityFile,
                                                           Status = application.Status
                                                       };

            if (applicationParameters.Status == 0 || applicationParameters.Status == 1 || applicationParameters.Status == 2)
            {
                query = query.Where(a => a.Status == applicationParameters.Status);
            }


            var applicationList = await query.ToListAsync();
            return new SuccessDataResult<List<ApplicationOutputModel>>(applicationList);
        }

 

        [HttpGet("byuser")]
        public async Task<IDataResult<List<ApplicationOutputModel>>> GetUserApplicationsAsync([FromQuery] string userId, [FromQuery] int status)
        {
            IQueryable<ApplicationOutputModel> query = from course in _context.Courses
                                                       join application in _context.Applications on course.Id equals application.CourseId
                                                       join user in _context.Users on application.UserId equals user.Id
                                                       where application.Status == status && user.Id == userId
                                                       select new ApplicationOutputModel
                                                       {
                                                           ApplicationId = application.ApplicationId,
                                                           CourseId = course.Id,
                                                           UserId = user.Id,
                                                           UserName = user.UserName,
                                                           FirstName = user.FirstName,
                                                           LastName = user.LastName,
                                                           Gender = user.Gender,
                                                           BirthDate = user.BirthDate,
                                                           City = user.City,
                                                           District = user.District,
                                                           Address = user.Address,
                                                           HighSchool = user.HighSchool,
                                                           HighSchoolGradiationDate = user.HighSchoolGradiationDate,
                                                           HighSchoolFile = user.HighSchoolFile,
                                                           University = user.University,
                                                           UniversityDepartment = user.UniversityDepartment,
                                                           UniversityGradiationDate = user.UniversityGradiationDate,
                                                           UniversityFile = user.UniversityFile,
                                                           Status = application.Status
                                                       };


            var applicationList = await query.ToListAsync();
            return new SuccessDataResult<List<ApplicationOutputModel>>(applicationList);
        }

        [HttpPost]
        public async Task<IActionResult> CreateOneApplicationAsync([FromBody] ApplicationCreateModel createModel)
        {
            if (createModel is null)
                return BadRequest(new ErrorResult("Model boş olamaz.")); //400         

            var application = await _manager.ApplicationService.CreateOneApplicationAsync(createModel);
            return Ok(new SuccessDataResult<ApplicationOutputModel>(application));
        }

        [HttpPut("status")]
        public async Task<IActionResult> UpdateStatusAsync([FromQuery] int id, [FromQuery] byte status)
        {
            await _manager.ApplicationService.UpdateStatusAsync(id, status);

            return StatusCode(200, new SuccessResult());

        }

        private async Task<IDataResult<List<ApplicationOutputModel>>> GetApplicationsForStudentAsync(string userId,byte? status)
        {

            IQueryable<ApplicationOutputModel> query = from course in _context.Courses
                                                       join application in _context.Applications on course.Id equals application.CourseId
                                                       join user in _context.Users on application.UserId equals user.Id
                                                       where user.Id == userId 

                                                       select new ApplicationOutputModel
                                                       {
                                                           ApplicationId = application.ApplicationId,
                                                           CourseId = course.Id,
                                                           UserId = user.Id,
                                                           UserName = user.UserName,
                                                           FirstName = user.FirstName,
                                                           LastName = user.LastName,
                                                           Gender = user.Gender,
                                                           BirthDate = user.BirthDate,
                                                           City = user.City,
                                                           District = user.District,
                                                           Address = user.Address,
                                                           HighSchool = user.HighSchool,
                                                           HighSchoolGradiationDate = user.HighSchoolGradiationDate,
                                                           HighSchoolFile = user.HighSchoolFile,
                                                           University = user.University,
                                                           UniversityDepartment = user.UniversityDepartment,
                                                           UniversityGradiationDate = user.UniversityGradiationDate,
                                                           UniversityFile = user.UniversityFile,
                                                           Status = application.Status,
                                                           IdentifyNumber = user.IdentifyNumber,
                                                           PhoneNumber = user.PhoneNumber,
                                                           Email = user.Email,
                                                           CourseName = course.Name,
                                                       };

            var applicationList = await query.ToListAsync();
            
            if (status != null )
            {
                applicationList = applicationList.Where( a => a.Status == status ).ToList();
            }
            return new SuccessDataResult<List<ApplicationOutputModel>>(applicationList);

        }
        private async Task<IDataResult<List<ApplicationOutputModel>>> GetApplicationsForAdmin(byte? status)
        {
            IQueryable<ApplicationOutputModel> query = from course in _context.Courses
                                                       join application in _context.Applications on course.Id equals application.CourseId
                                                       join user in _context.Users on application.UserId equals user.Id
                                                       select new ApplicationOutputModel
                                                       {
                                                           ApplicationId = application.ApplicationId,
                                                           CourseId = course.Id,
                                                           UserId = user.Id,
                                                           UserName = user.UserName,
                                                           FirstName = user.FirstName,
                                                           LastName = user.LastName,
                                                           Gender = user.Gender,
                                                           BirthDate = user.BirthDate,
                                                           City = user.City,
                                                           District = user.District,
                                                           Address = user.Address,
                                                           HighSchool = user.HighSchool,
                                                           HighSchoolGradiationDate = user.HighSchoolGradiationDate,
                                                           HighSchoolFile = user.HighSchoolFile,
                                                           University = user.University,
                                                           UniversityDepartment = user.UniversityDepartment,
                                                           UniversityGradiationDate = user.UniversityGradiationDate,
                                                           UniversityFile = user.UniversityFile,
                                                           Status = application.Status,
                                                           IdentifyNumber = user.IdentifyNumber,
                                                           PhoneNumber = user.PhoneNumber,
                                                           CourseName = course.Name,
                                                           Email = user.Email,
                                                       };


            var applicationList = await query.ToListAsync();

            if (status != null)
            {
                applicationList = applicationList.Where(a => a.Status == status).ToList();
            }
            return new SuccessDataResult<List<ApplicationOutputModel>>(applicationList);
        }

        private async Task<IDataResult<List<ApplicationOutputModel>>> GetApplicationsForEducatorAsync(string userId, byte? status)
        {
            IQueryable<ApplicationOutputModel> query = from course in _context.Courses
                                                       join application in _context.Applications on course.Id equals application.CourseId
                                                       join user in _context.Users on application.UserId equals user.Id
                                                       select new ApplicationOutputModel
                                                       {
                                                           ApplicationId = application.ApplicationId,
                                                           CourseId = course.Id,
                                                           UserId = user.Id,
                                                           UserName = user.UserName,
                                                           FirstName = user.FirstName,
                                                           LastName = user.LastName,
                                                           Gender = user.Gender,
                                                           BirthDate = user.BirthDate,
                                                           City = user.City,
                                                           District = user.District,
                                                           Address = user.Address,
                                                           HighSchool = user.HighSchool,
                                                           HighSchoolGradiationDate = user.HighSchoolGradiationDate,
                                                           HighSchoolFile = user.HighSchoolFile,
                                                           University = user.University,
                                                           UniversityDepartment = user.UniversityDepartment,
                                                           UniversityGradiationDate = user.UniversityGradiationDate,
                                                           UniversityFile = user.UniversityFile,
                                                           Status = application.Status,
                                                           IdentifyNumber = user.IdentifyNumber,
                                                           PhoneNumber = user.PhoneNumber,
                                                           CourseName = course.Name,
                                                           Email = user.Email

                                                       };

            var educatorcourselist = await _context.Courses.Where(x => x.CourseEducaterId.Equals(userId)).ToListAsync();
            
            var educatorcourselistids = educatorcourselist.Select(x=>x.Id).ToList();
            
            var applicationList = await query.ToListAsync();

            var outputapplicationList = applicationList.Where(x => educatorcourselistids.Contains(x.CourseId)).ToList();


            if (status != null)
            {
                outputapplicationList = outputapplicationList.Where(a => a.Status == status).ToList();
            }

            return new SuccessDataResult<List<ApplicationOutputModel>>(outputapplicationList);
        }

    }
}
