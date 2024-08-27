using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Exceptions.Applications
{
    public class ApplicationNotFoundException : NotFoundException
    {
        public ApplicationNotFoundException(int id)
          : base($"The application with id: {id} could not found.")
        {
        }
    }
}
