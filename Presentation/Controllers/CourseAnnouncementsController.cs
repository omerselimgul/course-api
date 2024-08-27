using AutoMapper;
using Core.InputOutputModels.Announcement;
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
    [Route("api/courseAnnouncement")]
    public class CourseAnnouncementsController
    {
        private readonly IMapper _mapper;
        protected readonly RepositoryContext _context;

        public CourseAnnouncementsController(IMapper mapper, RepositoryContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        [HttpGet("byCourse/{courseId}")]
        public async Task<IDataResult<List<CourseAnnouncementOutputModel>>> GetAnnouncementsByCourseIdAsync(int courseId)
        {
            var announcements = await _context.CourseAnnouncements
                .Where(ca => ca.CourseId == courseId) // Belirtilen courseId'ye göre duyuruları filtrele
                .ToListAsync();

            if (announcements == null || !announcements.Any())
            {
                return new ErrorDataResult<List<CourseAnnouncementOutputModel>>("Bu kurs için duyuru bulunamadı.");
            }

            var announcementOutputModel = _mapper.Map<List<CourseAnnouncementOutputModel>>(announcements);
            return new SuccessDataResult<List<CourseAnnouncementOutputModel>>(announcementOutputModel);
        }

            [HttpPost]
            public async Task<IDataResult<CourseAnnouncement>> CreateOneCourseAnnouncementAsync([FromBody] CourseAnnouncementInputModel inputModel)
            {
            
            var announcementEntity = _mapper.Map<CourseAnnouncement>(inputModel); 

                // Veritabanına ekleme işlemi
                _context.CourseAnnouncements.Add(announcementEntity);
                await _context.SaveChangesAsync();

                return new SuccessDataResult<CourseAnnouncement>(announcementEntity);
            }

        [HttpPut("{id}")]
        public async Task<IDataResult<CourseAnnouncementOutputModel>> UpdateAnnouncementAsync(int id, [FromBody] CourseAnnouncementInputModel inputModel)
        {
            var announcementEntity = await _context.CourseAnnouncements.FindAsync(id); 
            if (announcementEntity == null)
            {
                return new ErrorDataResult<CourseAnnouncementOutputModel>("Duyuru bulunamadı.");
            }


            _mapper.Map(inputModel, announcementEntity);


            _context.CourseAnnouncements.Update(announcementEntity);
            await _context.SaveChangesAsync();

            var announcementOutputModel = _mapper.Map<CourseAnnouncementOutputModel>(announcementEntity);
            return new SuccessDataResult<CourseAnnouncementOutputModel>(announcementOutputModel);
        }
    }
}
