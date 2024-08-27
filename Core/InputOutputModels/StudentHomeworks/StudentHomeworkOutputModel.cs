using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.InputOutputModels.StudentHomeworks
{
    public class StudentHomeworkOutputModel
    {
        public int Id { get; set; }
        public int HomeWorkId { get; set; }
        public string UserId { get; set; }
        public string? HomeWorkFile { get; set; }
        public DateTime? SendDate { get; set; }
        public byte? Status { get; set; }
        public float? Score { get; set; }
    }
    public class StudentHomeworkWithHomeWorkOutputModel
    {
        public int? StudentHomeworkId { get; set; }
        public string? UserId { get; set; }
        public string? HomeWorkFile { get; set; }
        public DateTime? SendDate { get; set; }
        public string? StudentHomeWorkStatus { get; set; }
        public float? Score { get; set; }

        public int HomeWorkId { get; set; }
        public string EducatorId { get; set; }
        public int CourseId { get; set; }
        public string? HomeworkHeader { get; set; }
        public string? Description { get; set; }
        public string? AssigmentFile { get; set; }
        public DateTime? DateofAssigment { get; set; }
        public DateTime? DeadlineDate { get; set; }
        public byte? HomeWorkStatus { get; set; }
        public bool? WorkStatus { get; set; }
    }
}

