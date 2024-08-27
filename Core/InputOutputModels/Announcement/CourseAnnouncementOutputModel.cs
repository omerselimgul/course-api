using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.InputOutputModels.Announcement
{
    public class CourseAnnouncementOutputModel
    {
        public int Id { get; set; }
        public int CourseId { get; set; }
        public string? Header { get; set; }

        public string? Description { get; set; }
        public DateTime? AnnouncementDate { get; set; }
    }
}
