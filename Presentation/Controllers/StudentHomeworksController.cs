using AutoMapper;
using Core.InputOutputModels.StudentHomeworks;
using Core.Utilities.Results;
using Entities.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Repositories.EFCore;
using Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/studentHomeworks")]
    public class StudentHomeworksController : ControllerBase
    {
        private readonly IServiceManager _manager;
        private readonly IMapper _mapper;
        protected readonly RepositoryContext _context;
        public StudentHomeworksController(IServiceManager manager, IMapper mapper, RepositoryContext context)
        {
            _manager = manager;
            _mapper = mapper;
            _context = context;
        }

        [HttpGet("allhomework")]
        public async Task<IDataResult<List<StudentHomeworkWithHomeWorkOutputModel>>> GetHomeworksByCourseId([FromQuery] int courseid)
        {
            var listOfCourseStudent = await _context.Set<Application>().Where(t=>t.CourseId ==courseid && t.Status == 1).ToListAsync();

            var listOfHomeWorkFromDb = await _context.Set<HomeWork>().Where(t => t.CourseId == courseid).ToListAsync();

            var listOfHomeWorkIds = listOfHomeWorkFromDb.Select(x => x.Id).ToList();

            var listOfStudentHomeWorks = await _context.Set<StudentHomework>().Where(x => listOfHomeWorkIds.Contains(x.HomeWorkId)).ToListAsync();

            var output = new List<StudentHomeworkWithHomeWorkOutputModel>();


            List<StudentHomework> emptyStudentHomeworkList = new List<StudentHomework>();
            foreach (var l in listOfHomeWorkFromDb)
            {
                foreach (var s  in listOfCourseStudent)
                {
                    var check = listOfStudentHomeWorks.FirstOrDefault(t => t.HomeWorkId == l.Id && t.UserId == s.UserId);
                    if(check == null)
                    {
                        var newStudentHomeWork = new StudentHomework()
                        {
                            HomeWorkId = l.Id,
                            UserId = s.UserId,
                        };
                        emptyStudentHomeworkList.Add(newStudentHomeWork);
                    }
                }
            }
           
             _context.StudentHomeworks.AddRange(emptyStudentHomeworkList);

            await _context.SaveChangesAsync();


            foreach (var homeWork in listOfHomeWorkFromDb)
            {
                foreach(var student in listOfCourseStudent)
                {
                    var studentHomeWork = listOfStudentHomeWorks.FirstOrDefault(x => x.HomeWorkId == homeWork.Id && x.UserId ==student.UserId);


                    var newItem = new StudentHomeworkWithHomeWorkOutputModel();
                    newItem.HomeWorkId = homeWork.Id;
                    newItem.EducatorId = homeWork.EducatorId;
                    newItem.CourseId = homeWork.CourseId;
                    newItem.HomeworkHeader = homeWork.HomeworkHeader;
                    newItem.Description = homeWork.Description;
                    newItem.AssigmentFile = homeWork.AssigmentFile;
                    newItem.DateofAssigment = homeWork.DateofAssigment;
                    newItem.DeadlineDate = homeWork.DeadlineDate;
                    newItem.HomeWorkStatus = homeWork.Status;
                    newItem.UserId = student.UserId;

                    if (studentHomeWork != null)
                    {
                        newItem.HomeWorkFile = studentHomeWork?.HomeWorkFile;
                        newItem.Score = studentHomeWork?.Score;
                        newItem.SendDate = studentHomeWork?.SendDate;
                        newItem.StudentHomeworkId = studentHomeWork?.Id;
                        newItem.StudentHomeWorkStatus = studentHomeWork?.Status.ToString();

                    }
                    else
                    {
                        newItem.HomeWorkFile = null;
                        newItem.Score = null;
                        newItem.SendDate = null;
                        newItem.StudentHomeworkId = null;
                        newItem.StudentHomeWorkStatus = null;
                    }

                    output.Add(newItem);
                }
            }
            return new SuccessDataResult<List<StudentHomeworkWithHomeWorkOutputModel>>(output);
        }

        [HttpGet()]
        public async Task<IDataResult<List<StudentHomeworkWithHomeWorkOutputModel>>> GetHomeworksByUserId([FromQuery] string userid, [FromQuery] int courseid)
        {
            var courseHomeWorkList =await _context.Set<HomeWork>().Where(x => x.CourseId == courseid).ToListAsync();
            var courseHomeWorkListId = courseHomeWorkList.Select(x => x.Id).ToList();
            var studentHomeWorkList = await _context.Set<StudentHomework>().Where(x => courseHomeWorkListId.Contains(x.HomeWorkId) && x.UserId == userid).ToListAsync();

            var output = new List<StudentHomeworkWithHomeWorkOutputModel>();
            foreach(var item in courseHomeWorkList)
            {
                var studentHomeWork = studentHomeWorkList.FirstOrDefault(t => t.HomeWorkId == item.Id);
                var newItem = new StudentHomeworkWithHomeWorkOutputModel
                {
                    HomeWorkId = item.Id,
                    EducatorId = item.EducatorId,
                    CourseId = item.CourseId,
                    HomeworkHeader = item.HomeworkHeader,
                    Description = item.Description,
                    AssigmentFile = item.AssigmentFile,
                    DateofAssigment = item.DateofAssigment,
                    DeadlineDate = item.DeadlineDate,
                    HomeWorkStatus = item.Status,

                    HomeWorkFile = studentHomeWork?.HomeWorkFile,
                    Score = studentHomeWork?.Score,
                    SendDate = studentHomeWork?.SendDate,
                    StudentHomeworkId = studentHomeWork?.Id,
                    WorkStatus = IsDateBetween(DateTime.Now, item.DateofAssigment, item.DeadlineDate),
                    StudentHomeWorkStatus = FındStatusHomeWorkStatus(studentHomeWork?.SendDate, item.DateofAssigment, item.DeadlineDate)

                };
                output.Add(newItem);
            }

            return new SuccessDataResult<List<StudentHomeworkWithHomeWorkOutputModel>>(output);

        }
        private static string FındStatusHomeWorkStatus(DateTime? sendDate, DateTime? dateofAssigment, DateTime? deadlineDate)
        {

            var result = IsDateBetween(DateTime.Now, dateofAssigment, deadlineDate);
            if (result == false)
            {
                
                if (sendDate != null)
                {
                    return "Yüklendi";
                }
                else if(dateofAssigment > DateTime.Now ) {
                    return "Atanadı";
                }
                else if (deadlineDate < DateTime.Now)
                {
                    return "Süresi doldu";
                }
            }
            else
            {
                if (sendDate != null)
                {
                    return "Yüklendi";
                }
                else{
                    return "Atandı";
                }
            }
            return "Atanadı";
        }
        private static bool IsDateBetween(DateTime? dateToCheck, DateTime? startDate, DateTime? endDate)
        {
            return dateToCheck >= startDate && dateToCheck <= endDate;
        }

        [HttpPost]
        public async Task<IResult> CreateOneHomeworkAsync([FromBody] StudentHomeworkInputModel inputModel)
        {
            if (inputModel is null)
                return new ErrorResult("400"); //400

            if(inputModel.Id == 0)
            {
                var model = _mapper.Map<StudentHomework>(inputModel);
                model.Status = 1;
                await _context.Set<StudentHomework>().AddAsync(model);
                await _context.SaveChangesAsync();
            }
       
            return new SuccessResult("Ödev başarıyla gönderildi");

        }

        [HttpPut("updatescores")]
        public async Task<IResult> UpdateScores([FromBody] List<StudentHomework> studentHomeworks)
        {
            if(studentHomeworks.Count> 0)
            {
                var updatedStudentHomeworkIds = studentHomeworks.Select(x => x.Id).ToList();
                var updatedStundentHomeWorkListDb = await _context.Set<StudentHomework>().Where(x => updatedStudentHomeworkIds.Contains(x.Id)).ToListAsync();

                foreach(var shw in updatedStundentHomeWorkListDb)
                {
                    var item = studentHomeworks.FirstOrDefault(x=>x.Id == shw.Id);
                    if(item != null)
                    {
                        if(item.Score > 100 || item.Score < 0)
                        {
                            return new ErrorResult("100 den büyük veya 0 dan küçük bir not girilemez");
                        }
                        shw.Score = item.Score;
                    }
                }
                await _context.SaveChangesAsync();
            }
            return new SuccessResult("Ödev başarıyla gönderildi");

        }
        //[HttpPut("{id:int}")]
        //public async Task<IActionResult> UpdateOneHomeworkAsync([FromRoute(Name = "id")] int id,
        //   [FromBody] StudentHomeworkInputModel inputModel)
        //{
        //    if (inputModel is null)
        //        return BadRequest();

        //    await _manager.StudentHomeworkService.UpdateOneHomeworkAsync(id, inputModel, true);
        //    return NoContent(); //204
        //}

        //[HttpDelete("{id:int}")]
        //public async Task<IActionResult> DeleteOneHomeworkAsync([FromRoute(Name = "id")] int id)
        //{
        //    await _manager.StudentHomeworkService.DeleteOneHomeworkAsync(id, false);
        //    return NoContent();
        //}

        //[HttpPut("status")]
        //public async Task<IActionResult> UpdateStatusAsync([FromQuery] int id, [FromQuery] byte status)
        //{
        //    await _manager.StudentHomeworkService.UpdateStatusAsync(id, status);

        //    return StatusCode(200, new SuccessResult());

        //}
    }
}
