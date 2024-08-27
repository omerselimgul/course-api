using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.InputOutputModels.Applications
{
    public class ApplicationCreateModel
    {
        public int CourseId { get; set; }
        public string UserId { get; set; }
        public byte Status { get; set; }
        public string? UserName { get; init; }
        public String? FirstName { get; set; }
        public String? LastName { get; set; }
        public string? Gender { get; set; }
        public DateTime? BirthDate { get; set; }
        public string? City { get; set; }
        public string? District { get; set; }
        public string? Address { get; set; }
        public string? HighSchool { get; set; }
        public DateTime? HighSchoolGradiationDate { get; set; }
        public string? HighSchoolFile { get; set; }
        public string? University { get; set; }
        public string? UniversityDepartment { get; set; }
        public DateTime? UniversityGradiationDate { get; set; }
        public string? UniversityFile { get; set; }
        public Int64? IdentifyNumber { get; set; }
        public string? PhoneNumber { get; set; }

    }
}
