using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Contracts.StudentHomeworks
{
    public interface IStudentHomeworkRepository : IRepositoryBase<StudentHomework>
    {
        Task<IEnumerable<StudentHomework>> GetAllHomeworksAsync(bool trackChanges);
        Task<StudentHomework> GetOneHomeworkByIdAsync(int id, bool trackChanges);
        void CreateOneHomework(StudentHomework homework);
        void UpdateOneHomework(StudentHomework homework);
        void DeleteOneHomework(StudentHomework homework);
    }
}
