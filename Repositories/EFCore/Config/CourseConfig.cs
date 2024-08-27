using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.EFCore.Config
{
    public class CourseConfig : IEntityTypeConfiguration<Course>
    {
        public void Configure(EntityTypeBuilder<Course> builder)
        {
            //builder.HasData(
            //    new Course { Id = 1, Name="Java Yazılım Geliştiricisi Eğitim Programı",CourseDuration=480},
            //    new Course { Id = 2, Name = "Veri Bilimi Uzmanlığı Programı", CourseDuration = 480 },
            //    new Course { Id = 3, Name = "Web ve Mobil Uygulama Geliştiricisi Eğitim Programı",CourseDuration=480 }
            //    );
        }
    }
}
