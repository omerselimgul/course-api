using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Exceptions.Educators
{
    public sealed class EducatorNotFoundExcepiton : NotFoundException
    {
        public EducatorNotFoundExcepiton(int id)
            : base($"The educator with id: {id} could not found.")
        {
        }
    }
}
