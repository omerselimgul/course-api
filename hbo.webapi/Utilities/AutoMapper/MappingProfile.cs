using AutoMapper;
using Core.InputOutputModels.Announcement;
using Core.InputOutputModels.Applications;
using Core.InputOutputModels.CourseDetails;
using Core.InputOutputModels.Courses;
using Core.InputOutputModels.Educators;
using Core.InputOutputModels.HomeWork;
using Core.InputOutputModels.StudentHomeworks;
using Core.InputOutputModels.Users;
using Entities.DataTransferObjects.Courses;
using Entities.Models;

namespace hbo.webapi.Utilities.AutoMapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile() 
        {
                   
            CreateMap<CourseDtoForUpdate, Course>();
            CreateMap<CourseDtoForInsertion, Course>();
          
            CreateMap<UserForRegistrationModel, User>();


            CreateMap<ApplicationInputModel, Application>();
            CreateMap<ApplicationCreateModel, Application>();
            CreateMap<Application, ApplicationOutputModel>();
     


            CreateMap<CourseDetailInputModel, CourseDetail>();
            CreateMap<CourseDetail, CourseDetailOutputModel>();

            CreateMap<CourseInputModel, Course>();
            CreateMap<Course, CourseOutputModel>();

            CreateMap<UserInfoInputModel,User >();

            CreateMap<User, EducatorOutputModel>();

            CreateMap<User,UserInfoOutputModel>();

            CreateMap<StudentHomeworkInputModel, StudentHomework>();
            CreateMap<StudentHomework, StudentHomeworkOutputModel>();


            CreateMap<AnnouncementInputModel, Announcement>();
            CreateMap<Announcement, AnnouncementOutputModel>();

            CreateMap<CourseAnnouncementInputModel, CourseAnnouncement>();
            CreateMap<CourseAnnouncement, CourseAnnouncementOutputModel>();

            CreateMap<ApplicationCreateModel, User>();


            CreateMap<HomeWork,OutputHomeWork>();
            CreateMap<InputHomeWork, HomeWork>();




        }
    }
}
