using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.InputOutputModels.Announcement
{
    public class CourseAnnouncementInputModel
    {
        public int CourseId { get; set; }
        public string? Header { get; set; }

        public string? Description { get; set; }
        public DateTime? AnnouncementDate  { get; set; }


        public CourseAnnouncementInputModel()
        {
            AnnouncementDate = DateTime.Now;
        }
    }
}
