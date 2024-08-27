using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DataTransferObjects.Courses
{
    public abstract record CourseDtoForManipulation
    {
        [Required(ErrorMessage ="Name is a required field.")]
        [MinLength(1)]
        [MaxLength(150)]
        public string Name { get; init; }
        public int CourseDuration { get; init; }

    }
}
