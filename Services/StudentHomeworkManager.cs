using AutoMapper;
using Core.InputOutputModels.StudentHomeworks;
using Entities.Exceptions;
using Entities.Exceptions.Homeworks;
using Entities.Models;
using Repositories.Contracts;
using Services.Contracts;
using Services.Contracts.Homeworks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class StudentHomeworkManager : IStudentHomeworkService
    {
        private readonly IRepositoryManager _manager;
        private readonly ILoggerService _logger;
        private readonly IMapper _mapper;

        public StudentHomeworkManager(IRepositoryManager manager,
            ILoggerService logger,
            IMapper mapper)
        {
            _manager = manager;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<IEnumerable<StudentHomeworkOutputModel>> GetAllHomeworksAsync(bool trackChanges)
        {
            var homeworks = await _manager.StudentHomework.GetAllHomeworksAsync(trackChanges);
            return _mapper.Map<IEnumerable<StudentHomeworkOutputModel>>(homeworks);
        }

        public async Task<StudentHomeworkOutputModel> GetOneHomeworkByIdAsync(int id, bool trackChanges)
        {
            var homework = await _manager.StudentHomework.GetOneHomeworkByIdAsync(id, trackChanges);
            if (homework is null)
                throw new Exception("Belirtilen id numarasına sahip ödev bulunamadı.");
            return _mapper.Map<StudentHomeworkOutputModel>(homework);
        }

        public async Task<StudentHomeworkOutputModel> CreateOneHomeworkAsync(StudentHomeworkInputModel createModel)
        {
            var entity = _mapper.Map<StudentHomework>(createModel);

            _manager.StudentHomework.CreateOneHomework(entity);
            await _manager.SaveAsync();
            return _mapper.Map<StudentHomeworkOutputModel>(entity);
        }


        public async Task UpdateStatusAsync(int id, byte status)
        {
            var fromDB = await _manager.StudentHomework.GetOneHomeworkByIdAsync(id, false);

            if (fromDB is null) throw new ArgumentException(nameof(fromDB));
            fromDB.Status = status;
            _manager.StudentHomework.Update(fromDB);
            await _manager.SaveAsync();
        }

        public async Task DeleteOneHomeworkAsync(int id, bool trackChanges)
        {
            var entity = await _manager.StudentHomework.GetOneHomeworkByIdAsync(id, trackChanges);
            if (entity is null)
                throw new HomeworkNotFoundExcepiton(id);
            _manager.StudentHomework.DeleteOneHomework(entity);
            await _manager.SaveAsync();
        }

        public async Task UpdateOneHomeworkAsync(int id, StudentHomeworkInputModel inputModel, bool trackChanges)
        {
            var entity = await _manager.StudentHomework.GetOneHomeworkByIdAsync(id, trackChanges);

            if (entity is null)
                throw new HomeworkNotFoundExcepiton(id);

            //Mapping
            entity = _mapper.Map<StudentHomework>(inputModel);

            _manager.StudentHomework.Update(entity);
            await _manager.SaveAsync();

        }


    }
}
