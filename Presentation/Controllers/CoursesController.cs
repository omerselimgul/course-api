using AutoMapper;
using Core.InputOutputModels.Applications;
using Core.InputOutputModels.Courses;
using Core.Utilities.Results;
using Entities.Exceptions;
using Entities.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Repositories.Contracts;
using Repositories.EFCore;
using Services.Contracts;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace Presentation.Controllers
{
	[ApiController]
	[Route("api/courses")]
	public class CoursesController : ControllerBase
	{
		private readonly IServiceManager _manager;
        private readonly UserManager<User> _userManager;
        protected readonly RepositoryContext _context;
        private readonly IMapper _mapper;


        public CoursesController(
            IServiceManager manager, 
            UserManager<User> userManager, 
            RepositoryContext context,
            IMapper mapper)
		{
			_manager = manager;
            _userManager = userManager;
            _context = context;
            _mapper = mapper;


        }
        [HttpGet("Coursestudentcontrol")]
        public async Task<IResult> Coursestudentcontrol([FromQuery] string userId, [FromQuery] int courseid)
        {
            var userInfo = await _context.Set<User>().FirstOrDefaultAsync(x => x.Id.Equals(userId));
            var roles = await _userManager.GetRolesAsync(userInfo);
            var rolesList = roles.ToList();

            if (rolesList.Any(x => x.Equals("Admin")))
            {
                return new SuccessResult("Admin");

            }
            var courseInfo = await _context.Courses.FirstOrDefaultAsync(x => x.Id == courseid);
            if (courseInfo == null)
            {
                return new ErrorResult("Sayfa bulumamadı");
            }
            if (courseInfo.CourseEducaterId.Equals(userId))
                return new SuccessResult("Eğitmen");

            var userControl = await _context.Applications.FirstOrDefaultAsync(x => x.CourseId == courseid && x.UserId == userId && x.Status == 1);
            if (userControl == null)
                return new ErrorResult("Sayfa bulumamadı");
            else return new SuccessResult("Öğrenci");
        }

        [HttpGet("geybyuserid")]
        public async Task<IDataResult<List<CourseOutputModel>>> GetAllCourseByUserIdAsync([FromQuery] string userId)
        {
            var userInfo = await _context.Set<User>().FirstOrDefaultAsync(x => x.Id.Equals(userId));


            var roles = await _userManager.GetRolesAsync(userInfo);
            var rolesList = roles.ToList();
          
			var courses = await _manager.CourseService.GetAllCoursesAsync(false);
            var courseList = courses.ToList();


            if (rolesList.Any(x => x.Equals("Admin")))
            {
                return new SuccessDataResult<List<CourseOutputModel>>(courseList);

            }
            else if (rolesList.Any(x => x.Equals("Student")))
            {
                //basvurusu onaylanmis statusu 1 olmus kursların liste gelcek 

                var applications = await _context.Applications
                    .Where(a => a.UserId == userId && a.Status == 1)
                     .ToListAsync();
               
                // Başvuruların her biri için ilgili kurs bilgisini çek
                var courseIds = applications.Select(a => a.CourseId).ToList();
                var studentCourses = await _context.Courses
                    .Where(c => courseIds.Contains(c.Id))
                    .ToListAsync();

                var courseOutputModels = _mapper.Map<List<CourseOutputModel>>(studentCourses);
                return new SuccessDataResult<List<CourseOutputModel>>(courseOutputModels);


            }
            else if (rolesList.Any(x => x.Equals("Educator")))
            {
                var educatorCourses = await _context.Courses
                    .Where(c => c.CourseEducaterId == userId)
                    .ToListAsync();

                var courseOutputModels = _mapper.Map<List<CourseOutputModel>>(educatorCourses);
                return new SuccessDataResult<List<CourseOutputModel>>(courseOutputModels);
            }
            return new ErrorDataResult<List<CourseOutputModel>>("Kullanici bulunamadi.");
        }


        //[Authorize(Roles = "Admin,Educator,Student")]
        [HttpGet]
		public async Task<IDataResult<List<CourseOutputModel>>> GetAllCoursesAsync()
		{
			var courses = await _manager.CourseService.GetAllCoursesAsync(false);
			var courseList = courses.ToList();	
			return new SuccessDataResult<List<CourseOutputModel>>(courseList);
		}

		//[Authorize(Roles = "Admin,Educator")]
		[HttpGet("{id:int}")]
		public async Task<IActionResult> GetOneCourse([FromRoute(Name = "id")] int id)
		{
            var course = await _manager
                .CourseService
				.GetOneCourseByIdAsync(id, false);
           
			return StatusCode(200, new SuccessDataResult<CourseOutputModel>(course));
            //return new SuccessDataResult<CourseOutputModel>(course);
		}

		//[Authorize(Roles = "Admin,Educator")]
		[HttpPost]
		public async Task<IDataResult<Course>> CreateOneCourseAsync([FromBody] CourseInputModel inputModel)
		{
			if (inputModel is null)
				return new ErrorDataResult<Course>("model null olamaz");

			var course = await _manager.CourseService.CreateOneCourseAsync(inputModel);
			return new SuccessDataResult<Course>(course);
		}

		//[Authorize(Roles = "Admin,Educator")]
		[HttpPut("{id:int}")]
		public async Task<IResult> UpdateOneCourseAsync([FromRoute(Name = "id")] int id,
		   [FromBody] CourseInputModel courseInputModel)
		{
			if (courseInputModel is null)
				return new ErrorResult("Model Boş olamaz");

			await _manager.CourseService.UpdateOneCourseAsync(id, courseInputModel);
			return new SuccessResult();
		}

	}
}
