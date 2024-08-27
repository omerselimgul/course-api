using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Repositories.Contracts.StudentHomeworks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.EFCore.StudentHomeworks
{
    public class StudentHomeworkRepository : RepositoryBase<StudentHomework>, IStudentHomeworkRepository
    {
        public StudentHomeworkRepository(RepositoryContext context) : base(context)
        {
        }

        public async Task<IEnumerable<StudentHomework>> GetAllHomeworksAsync(bool trackChanges) =>
            await FindAll(trackChanges)
            .ToListAsync();

        public async Task<StudentHomework> GetOneHomeworkByIdAsync(int id, bool trackChanges) =>
            await FindByCondition(c => c.Id.Equals(id), trackChanges)
            .SingleOrDefaultAsync();

        public void UpdateOneHomework(StudentHomework homework) => Update(homework);
        public void CreateOneHomework(StudentHomework homework) => Create(homework);
        public void DeleteOneHomework(StudentHomework homework) => Delete(homework);


    }
}
