using Entities.DataTransferObjects.Courses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class CourseDetail
    {
        public int? WeekNo { get; set; }
        public int Id { get; set; }
        public int CourseId { get; set; }
        public string? Issue { get; set; }
        public string? Content { get; set; }
        public string? Aim { get; set; }
        public string? Method { get; set; }
        public string? Activity { get; set; }
        public Course Course { get; set; }
    }
}
