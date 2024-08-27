using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.InputOutputModels.CourseDetails
{
    public class CourseDetailInputModel
    {

        public int? WeekNo { get; set; }
        public int? Id { get; set; }
        public int? CourseId { get; set; }
        public string? Issue { get; set; }
        public string? Content { get; set; }
        public string? Aim { get; set; }
        public string? Method { get; set; }
        public string? Activity { get; set; }
    }
}
