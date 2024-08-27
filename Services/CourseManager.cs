using AutoMapper;
using Core.InputOutputModels.CourseDetails;
using Core.InputOutputModels.Courses;
using Entities.DataTransferObjects.Courses;
using Entities.Exceptions;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Repositories.Contracts;
using Repositories.EFCore;
using Services.Contracts;
using Services.Contracts.Courses;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class CourseManager : ICourseService
    {
        private readonly IRepositoryManager _manager;
        private readonly ILoggerService _logger;
		private readonly IMapper _mapper;
        protected readonly RepositoryContext _context;

        public CourseManager(IRepositoryManager manager,
            ILoggerService logger, 
            IMapper mapper,
            RepositoryContext context)
        {
            _manager = manager;
			_logger = logger;
			_mapper = mapper;
            _context = context;
        }
        public async Task<IEnumerable<CourseOutputModel>> GetAllCoursesAsync(bool trackChanges)
        {
            var courses = await _manager.Course.GetAllCoursesAsync(trackChanges);
            return _mapper.Map<IEnumerable<CourseOutputModel>>(courses);
        }

        public async Task<CourseOutputModel> GetOneCourseByIdAsync(int id, bool trackChanges)
        {
            var course = await _manager.Course.GetOneCourseByIdAsync(id, trackChanges) ?? throw new CourseNotFoundExcepiton(id);

            var mapped = _mapper.Map<CourseOutputModel>(course);

            var courseDetails = await _context.Set<CourseDetail>().Where(x => x.CourseId == id).ToListAsync();

            mapped.CourseDetails = _mapper.Map<List<CourseDetailOutputModel>>(courseDetails);

            return mapped;
        }

        public async Task<Course> CreateOneCourseAsync(CourseInputModel model)
        {


            var item = _mapper.Map<Course>(model);
            item.CourseDetails = null;
            _manager.Course.CreateOneCourse(item);
            await _manager.SaveAsync();

            var newListOfCourseDetial = new List<CourseDetail>();
            foreach (var courseDetial in model.CourseDetails)
            {

                var mapped = _mapper.Map<CourseDetail>(courseDetial);
                mapped.CourseId = item.Id;
                mapped.Id = 0;
                newListOfCourseDetial.Add(mapped);
            }

            _context.Set<CourseDetail>().AddRange(newListOfCourseDetial);
            await _manager.SaveAsync();

            //if (model.CourseEducater is not null)
            //{
            //    await _context.Set<CourseEducater>().AddAsync(new CourseEducater() { CourseId = item.Id, EducaterId = model.CourseEducater });
            //}
            //await _manager.SaveAsync();

            return item;
        }

        public async Task UpdateOneCourseAsync(int id, CourseInputModel model)
        {
            var entity = await _manager.Course.GetOneCourseByIdAsync(id, false);

            if (entity is null)
                throw new CourseNotFoundExcepiton(id);
            
			//Mapping
			var courseEntity = _mapper.Map<Course>(model);
            courseEntity.CourseDetails = null;

            _manager.Course.Update(courseEntity);

            
            //var educaterItem = await _context.Set<CourseEducater>().FirstOrDefaultAsync(x=>x.CourseId == id);
            //if(educaterItem!= null && educaterItem.EducaterId != model.CourseEducater)
            //{
            //    educaterItem.EducaterId = model.CourseEducater;
            //    _context.Set<CourseEducater>().Update(educaterItem);
            //}else if(educaterItem == null && model.CourseEducater != null)
            //{
            //    _context.Set<CourseEducater>().Add(new CourseEducater() { CourseId = id, EducaterId = model.CourseEducater });
            //}

            await _context.SaveChangesAsync();

            var courseDetailEntity = _mapper.Map<List<CourseDetailInputModel>, List<CourseDetail>>(model.CourseDetails.ToList());


            var listOfCourseDetail = await _context.Set<CourseDetail>().AsNoTracking().Where(x => x.CourseId == id).ToListAsync();

            var updatedList = new List<CourseDetail>();
            var newList = new List<CourseDetail>();
            var rmItem = new List<CourseDetail>();
            for (int i = 0; i< listOfCourseDetail.Count; i++)
            {
                var updatedEntity = courseDetailEntity.FirstOrDefault(x => x.Id == listOfCourseDetail[i].Id);
                if (updatedEntity != null)
                {
                    updatedList.Add(updatedEntity);
                }
                else
                {
                    rmItem.Add(listOfCourseDetail[i]);
                }
            }

            newList = courseDetailEntity.Where(x => x.Id == 0).ToList();
            foreach (var item in newList)
            {
                item.CourseId = id;
            }
            _context.Set<CourseDetail>().RemoveRange(rmItem);
            await _context.SaveChangesAsync();

            _context.Set<CourseDetail>().UpdateRange(updatedList);
            await _context.SaveChangesAsync();

            _context.Set<CourseDetail>().AddRange(newList);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteOneCourseAsync(int id, bool trackChanges)
        {
            var entity = await _manager.Course.GetOneCourseByIdAsync(id, trackChanges);
            if (entity is null)
                throw new CourseNotFoundExcepiton(id);


            _manager.Course.DeleteOneCourse(entity);
            await _manager.SaveAsync();

        }

    }
}
