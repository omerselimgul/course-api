using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class HomeWork
    {
        public int Id { get; set; }
        public string EducatorId { get; set; }
        [Required]
        public int CourseId { get; set; }
        public string? HomeworkHeader { get; set; }
        public string? Description { get; set; }
        public string? AssigmentFile { get; set; }
        public DateTime? DateofAssigment { get; set; }
        public DateTime? DeadlineDate { get; set; }
        public byte? Status { get; set; }
        // STatus 0 pasif 1 aktif 2 out of date
    }
}
