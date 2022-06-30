using AutoMapper;
using TeacherAndClassRoomWebApi.ApiModel;
using TeachersAndClassroomDll.Entities;

namespace TeacherAndClassRoomWebApi.AutoMappingConfiguration
{
    public class AutoMappingModel:Profile
    {
        public AutoMappingModel()
        {
           CreateMap<Teacher, TeacherApiModel>().ReverseMap(); ;
           CreateMap<Classroom, ClassroomApiModel>().ReverseMap();
        }
    }
}
