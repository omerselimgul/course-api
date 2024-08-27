using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class Application
    {
        public int ApplicationId { get; set; }
        public int CourseId { get; set; }
        public string UserId {  get; set; }
        public byte Status { get; set; }
        public User User {  get; set; }
        public Course Course { get; set;}


    }
}
