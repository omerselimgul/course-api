﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class Announcement
    {
        public int Id { get; set; }
        public string Header { get; set; }

        public string Description { get; set; }
        public DateTime? AnnouncementDate { get; set; }
    }
}
