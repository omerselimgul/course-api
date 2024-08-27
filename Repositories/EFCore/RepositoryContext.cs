using Entities.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Repositories.EFCore.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.EFCore
{
    public class RepositoryContext : IdentityDbContext<User>
    {
        public RepositoryContext(DbContextOptions<RepositoryContext> options) : 
            base(options) 
        {
        }
        public DbSet<Course> Courses { get; set; }
        //public DbSet<WeekSchedule> WeekSchedules { get; set; }
        public DbSet<CourseDetail> CourseDetails { get; set; }
        public DbSet<Application> Applications { get; set; }
        public DbSet<EducatorHomework> EducatorHomeworks { get; set; }
        public DbSet<StudentHomework> StudentHomeworks { get; set; }

        public DbSet<CourseEducater> CourseEducaters { get; set; }
        public DbSet<Announcement> Announcements { get; set; }
        public DbSet<CourseAnnouncement> CourseAnnouncements { get; set; }
        public DbSet<HomeWork> HomeWorks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            //modelBuilder.ApplyConfiguration(new CourseConfig());
            //modelBuilder.ApplyConfiguration(new RoleConfiguration());
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

   

            modelBuilder.Entity<Application>();
            modelBuilder.Entity<Application>().HasKey(a => a.ApplicationId);

            modelBuilder.Entity<Application>().Property(a => a.ApplicationId).ValueGeneratedOnAdd();
            modelBuilder.Entity<Application>().Property(u => u.ApplicationId).Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Throw);

            modelBuilder.Entity<Application>()
                .HasKey(a=> new {a.CourseId, a.UserId});
        
            modelBuilder.Entity<Application>()
                .HasOne(a=>a.Course)
                .WithMany(b=>b.Users)
                .HasForeignKey(a=>a.CourseId);

            modelBuilder.Entity<Application>()
                .HasOne(a => a.User)
                .WithMany(b => b.Courses)
                .HasForeignKey(a => a.UserId);

            modelBuilder.Entity<CourseEducater>();

            //modelBuilder.Entity<CourseDetail>()
            //    .HasMany(cd => cd.WeekSchedules) // Bir CourseDetail'in birden fazla WeekSchedule'ı olabilir
            //    .WithOne(ws => ws.CourseDetail) // Bir WeekSchedule'ın bir CourseDetail'a ait olduğu belirtilir
            //    .HasForeignKey(ws => ws.CourseDetialId); // WeekSchedule tablosundaki CourseDetailId alanı ile ilişkilendirme yapılıyor

            //modelBuilder.Entity<WeekSchedule>()
            //    .HasKey(ws => ws.Id);


            //.HasOne(x => x.Course)
            //.WithMany(x => x.CourseDetails).HasForeignKey(x=>x.CourseId);

            //modelBuilder.Entity<WeekSchedule>();
            //.HasOne(x => x.Course)
            //.WithMany(x => x.WeekSchedules)


            modelBuilder.Entity<CourseDetail>()
                .HasKey(cd => cd.Id);




            modelBuilder.Entity<Course>().HasKey(c => c.Id);
            modelBuilder.Entity<Course>().HasMany(c => c.CourseDetails).WithOne(c => c.Course).HasForeignKey(c => c.CourseId);

            modelBuilder.Entity<HomeWork>().HasKey(c => c.Id);
        }
    }
}
