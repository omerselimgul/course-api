using Core.Utilities.Results;
using Entities.Models;
using Microsoft.AspNetCore.Mvc;
using Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/coursedetail")]
    public class CourseDetailsController
    {
        private readonly IServiceManager _manager;

        public CourseDetailsController(IServiceManager manager)
        {
            _manager = manager;
        }

        //[Authorize(Roles = "Admin,Educator,Student")]
        [HttpGet]
        public async Task<IDataResult<List<CourseDetail>>> GetAllCoursesAsync()
        {
            var courses = await _manager.CourseDetailService.GetAllAsync();
            var courseList = courses.ToList();
            return new SuccessDataResult<List<CourseDetail>>(courseList);
        }

        //[Authorize(Roles = "Admin,Educator")]
        //[HttpGet("{id:int}")]
        //public async Task<IDataResult<CourseDto>> GetOneCourse([FromRoute(Name = "id")] int id)
        //{
        //    var course = await _manager
        //        .CourseService
        //        .GetOneCourseByIdAsync(id, true);

        //    return new SuccessDataResult<CourseDto>(course);
        //}

        ////[Authorize(Roles = "Admin,Educator")]
        //[HttpPost]
        //public async Task<IActionResult> CreateOneCourseAsync([FromBody] CourseDtoForInsertion courseDto)
        //{
        //    if (courseDto is null)
        //        return BadRequest(); //400

        //    var course = await _manager.CourseService.CreateOneCourseAsync(courseDto);
        //    return StatusCode(201, course);

        //}

        ////[Authorize(Roles = "Admin,Educator")]
        //[HttpPut("{id:int}")]
        //public async Task<IActionResult> UpdateOneCourseAsync([FromRoute(Name = "id")] int id,
        //   [FromBody] CourseDtoForUpdate courseDto)
        //{
        //    if (courseDto is null)
        //        return BadRequest();

        //    await _manager.CourseService.UpdateOneCourseAsync(id, courseDto, true);
        //    return NoContent(); //204
        //}

        ////[Authorize(Roles = "Admin")]
        //[HttpDelete("{id:int}")]
        //public async Task<IActionResult> DeleteOneCourseAsync([FromRoute(Name = "id")] int id)
        //{
        //    await _manager.CourseService.DeleteOneCourseAsync(id, false);
        //    return NoContent();
        //}

    }
}
