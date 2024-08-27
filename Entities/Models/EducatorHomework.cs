using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class EducatorHomework
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public int CourseId { get; set; }
        public string? HomeworkHeader { get; set; }
        public string? Description { get; set; }
        public string? HomeWorkFile { get; set; }
        public DateTime? DateofAssigment { get; set; }
        public DateTime? DeadlineDate { get; set; }
        public byte? Status { get; set; }
    }
}
