using AutoMapper;
using Core.InputOutputModels.HomeWork;
using Core.Utilities.Results;
using Entities.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Repositories.EFCore;



namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/homework")]
    public class HomeWorkController { 

        private readonly IMapper _mapper;
        protected readonly RepositoryContext _context;

        public HomeWorkController(IMapper mapper, RepositoryContext context)
        {

          _mapper = mapper;
            _context = context;  
        }

        [HttpGet()]
        public async Task<IDataResult<List<OutputHomeWork>>> GetByUseridAndCourseid([FromQuery] int courseid)
        {
            var homeworkList = await _context.Set<HomeWork>().Where(x => x.CourseId == courseid).ToListAsync();

            var mapped = _mapper.Map<List<OutputHomeWork>>(homeworkList);
            return new SuccessDataResult<List<OutputHomeWork>>(mapped);
        }


        //[HttpGet("allhomework")]
        //public async Task<IActionResult> GetCourseid( [FromQuery] int courseid)
        //{
        //    var homeworkList = await _context.Set<HomeWork>().Where(x => x.CourseId == courseid  ).ToListAsync();


        //    var mapped = _mapper.Map<List<OutputHomeWork>>(homeworkList);
        //    var result = mapped.GroupBy(t => t.HomeWorkId);
        //    return new OkObjectResult(result);
        //    //return new SuccessDataResult<List<OutputHomeWork>>(mapped);
        //}


        [HttpPost()]
        public async Task<IDataResult<OutputHomeWork>> CreateHomeWorkAsync([FromBody] InputHomeWork inputHomeWork)
        {
            var homeWork = _mapper.Map<HomeWork>(inputHomeWork); 

            await _context.Set<HomeWork>().AddRangeAsync(homeWork);

            await _context.SaveChangesAsync();

            var mapped = _mapper.Map<OutputHomeWork>(homeWork);

            return new SuccessDataResult<OutputHomeWork>(mapped,"başarıyla ödev tanımlandı");
        }

        //[HttpPut()]
        //public async Task<IDataResult<OutputHomeWork>> UpdateHomeWorkAsync( [FromBody] InputHomeWork inputModel)
        //{
        //    var homeWork = await _context.Set<HomeWork>().FirstOrDefaultAsync(x => x.Id == inputModel.Id);
        //    homeWork.Status = 1;
        //    homeWork.HomeWorkFile = inputModel.HomeWorkFile;
        //    _context.Update(homeWork);
        //    await _context.SaveChangesAsync();
        //    var mapped = _mapper.Map<OutputHomeWork>(homeWork);
        //    return new SuccessDataResult<OutputHomeWork>(mapped);
        //}
    }
}
