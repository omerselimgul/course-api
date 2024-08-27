using AutoMapper;
using Core.InputOutputModels.Announcement;
using Core.InputOutputModels.Courses;
using Core.InputOutputModels.Educators;
using Core.Utilities.Results;
using Entities.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
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
    [Route("api/announcement")]
    public class AnnouncementsController
    {
        private readonly IServiceManager _service;
        private readonly IMapper _mapper;
        protected readonly RepositoryContext _context;


        public AnnouncementsController(IMapper mapper, RepositoryContext context)
        {
         
            _mapper = mapper;
            _context = context;
        }

        [HttpGet]
        public async Task<IDataResult<List<AnnouncementOutputModel>>> GetAllAnnouncementAsync()
        {
            var announcements = _context.Announcements.ToList(); // Tüm duyuruları veritabanından al
            var announcementOutputModel= _mapper.Map<List<AnnouncementOutputModel>>(announcements);
            return new SuccessDataResult<List<AnnouncementOutputModel>>(announcementOutputModel);

        }

        [HttpPost]
        public async Task<IDataResult<Announcement>> CreateOneAnnouncementAsync([FromBody] AnnouncementInputModel inputModel)
        {

            inputModel.AnnouncementDate = DateTime.Now;

            var announcementEntity = _mapper.Map<Announcement>(inputModel); // DTO'dan varlığa dönüşüm

            // Veritabanına ekleme işlemi
            _context.Announcements.Add(announcementEntity);
            await _context.SaveChangesAsync();

            return new SuccessDataResult<Announcement>(announcementEntity);
        }

        [HttpPut("{id}")]
        public async Task<IDataResult<AnnouncementOutputModel>> UpdateAnnouncementAsync(int id, [FromBody] AnnouncementInputModel inputModel)
        {
            var announcementEntity = await _context.Announcements.FindAsync(id); // ID'ye göre duyuru bul
            if (announcementEntity == null)
            {
                return new ErrorDataResult<AnnouncementOutputModel>("Duyuru bulunamadı.");
            }

     
            _mapper.Map(inputModel, announcementEntity);

            
            _context.Announcements.Update(announcementEntity);
            await _context.SaveChangesAsync();

            var announcementOutputModel = _mapper.Map<AnnouncementOutputModel>(announcementEntity);
            return new SuccessDataResult<AnnouncementOutputModel>(announcementOutputModel);
        }


    }
}
