using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.InputOutputModels.Announcement
{
    public class AnnouncementOutputModel
    {
        public int Id { get; set; }
        public string Header { get; set; }

        public string Description { get; set; }
        public DateTime? AnnouncementDate { get; set; }
    }
}
