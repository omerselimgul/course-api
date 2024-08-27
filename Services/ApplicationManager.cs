using AutoMapper;
using Core.InputOutputModels.Applications;
using Core.Utilities.Results;
using Entities.Models;
using Microsoft.AspNetCore.Identity;
using NLog.Internal.Fakeables;
using Repositories.Contracts;
using Repositories.EFCore;
using Services.Contracts;
using Services.Contracts.Applications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class ApplicationManager : IApplicationService
    {
        private readonly IRepositoryManager _manager;
        private readonly ILoggerService _logger;
        private readonly IMapper _mapper;
        protected readonly RepositoryContext _context;
        private readonly UserManager<User> _userManager;



        public ApplicationManager(
            UserManager<User> userManager,
            IRepositoryManager manager,
            ILoggerService logger,
            IMapper mapper,
            RepositoryContext context)
        {
            _userManager = userManager;
            _manager = manager;
            _logger = logger;
            _mapper = mapper;
            _context = context;
        }

        public async Task<ApplicationOutputModel> GetOneApplicationByIdAsync(int id, bool trackChanges)
        {
            var application = await _manager.Application.GetOneApplicationByIdAsync(id, trackChanges);
            if (application is null)
                throw new Exception("Belirtilen id numarasına sahip başvuru bulunamadı.");
            return _mapper.Map<ApplicationOutputModel>(application);
        }

        public async Task<ApplicationOutputModel> CreateOneApplicationAsync(ApplicationCreateModel createModel)
        {
            var applicationEntity = _mapper.Map<Application>(createModel);

            var user = await _userManager.FindByIdAsync(createModel.UserId);


            var mapped = _mapper.Map<ApplicationCreateModel, User>(createModel, user);
            mapped.UserName = user.UserName;

            var result = await _userManager.UpdateAsync(mapped);
            applicationEntity.User = user;
            applicationEntity.Course = _context.Set<Course>().SingleOrDefault(x=>x.Id == createModel.CourseId);
            _manager.Application.CreateOneApplication(applicationEntity);
            await _manager.SaveAsync();
            return _mapper.Map<ApplicationOutputModel>(applicationEntity);
        }

        public async Task UpdateStatusAsync(int id, byte status)
        {
            var fromDB = await _manager.Application.GetOneApplicationByIdAsync(id, false);

            if (fromDB is null) throw new ArgumentException(nameof(fromDB));
            fromDB.Status = status;
            _manager.Application.Update(fromDB);
            await _manager.SaveAsync();
        }
    }
}

