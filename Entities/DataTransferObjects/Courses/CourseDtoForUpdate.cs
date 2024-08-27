using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DataTransferObjects.Courses
{
    public record CourseDtoForUpdate : CourseDtoForManipulation
    {
        [Required]
        public int Id { get; init; }


    }
}
