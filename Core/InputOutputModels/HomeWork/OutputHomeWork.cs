using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.InputOutputModels.HomeWork
{
    public class OutputHomeWork
    {
        public int Id { get; set; }
        public string EducatorId { get; set; }
        public int CourseId { get; set; }
        public string? HomeworkHeader { get; set; }
        public string? Description { get; set; }
        public string? AssigmentFile { get; set; }
        public DateTime? DateofAssigment { get; set; }
        public DateTime? DeadlineDate { get; set; }
        public byte? Status { get; set; }
    }
}
