using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class StudentHomework
    {
        public int Id { get; set; }
        [Required]
        public int HomeWorkId { get; set; }
        public string UserId { get; set; }
        public string? HomeWorkFile { get; set; }
        public DateTime? SendDate { get; set; }
        public byte? Status { get; set; }
        public float? Score { get; set; }
    }
}
