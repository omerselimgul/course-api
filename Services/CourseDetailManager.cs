using AutoMapper;
using Entities.Models;
using Repositories.Contracts;
using Services.Contracts;
using Services.Contracts.CourseDetails;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class CourseDetailManager : ICourseDetailService 
    {
        private readonly IRepositoryManager _manager;
        private readonly ILoggerService _logger;
        private readonly IMapper _mapper;

        public CourseDetailManager(
            IRepositoryManager manager,
            ILoggerService logger,
            IMapper mapper)
        {
            _manager = manager;
            _logger = logger;
            _mapper = mapper;
        }

        public CourseDetail Add(CourseDetail entity)
        {
            _manager.CourseDetail.Create(entity);
            return entity;
        }

        public CourseDetail Delete(CourseDetail entity)
        {
            _manager.CourseDetail.Delete(entity);
            return entity;
        }

        public async Task<CourseDetail> Get(CourseDetail entity)
        {
            var result= await _manager.CourseDetail.GetOneCourseDetailByIdAsync(entity.Id,false);
            return result;
        }

        public async Task<IEnumerable<CourseDetail>> GetAllAsync()
        {
            var result = await _manager.CourseDetail.GetAllCourseDetailsAsync(false);
            return result;
        }

        public CourseDetail Update(CourseDetail entity)
        {
            _manager.CourseDetail.Update(entity);
            return entity;
        }
    }
}
