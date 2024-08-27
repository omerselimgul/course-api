using Core.InputOutputModels.CourseDetails;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.InputOutputModels.Courses
{

    public class CourseInputModel
    {
        public int? Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? CourseCode { get; set; }
        public long? MinimumQuota { get; set; }
        public long? MaximumQuota { get; set; }
        public string? DocumentType { get; set; }
        public DateTime RegisterBeginDate { get; set; }
        public DateTime RegisterEndDate { get; set; }
        public DateTime CourseBeginDate { get; set; }
        public DateTime CourseEndDate { get; set; }
        public string? PurposeOfCourse { get; set; }
        public ICollection<CourseDetailInputModel>? CourseDetails { get; set; }
        public string? CourseEducaterId { get; set; }
        public string? Coordinator { get; set; }
        public string? CourseDocument { get; set; }

    }
}
