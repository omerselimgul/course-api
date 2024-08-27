using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Exceptions.Homeworks
{
    public sealed class HomeworkNotFoundExcepiton : NotFoundException
    {
        public HomeworkNotFoundExcepiton(int id)
            : base($"The homework with id: {id} could not found.")
        {
        }
    }
}
