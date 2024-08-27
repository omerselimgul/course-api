using Core.InputOutputModels.StudentHomeworks;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Contracts.Homeworks
{
    public interface IStudentHomeworkService
    {
        Task<IEnumerable<StudentHomeworkOutputModel>> GetAllHomeworksAsync(bool trackChanges);
        Task<StudentHomeworkOutputModel> GetOneHomeworkByIdAsync(int id, bool trackChanges);
        Task<StudentHomeworkOutputModel> CreateOneHomeworkAsync(StudentHomeworkInputModel inputModel);
        Task UpdateOneHomeworkAsync(int id, StudentHomeworkInputModel inputModel, bool trackChanges);
        Task DeleteOneHomeworkAsync(int id, bool trackChanges);
        Task UpdateStatusAsync(int id, byte status);
    }
}
