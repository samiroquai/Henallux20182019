namespace api.Infrastructure
{

    public class MappingProfile : AutoMapper.Profile
    {


        public MappingProfile()
        {
            CreateMap<Model.Student, DTO.Student>().ReverseMap();
            CreateMap<Model.StudentCourse, DTO.StudentCourse>().ReverseMap();
            CreateMap<Model.Course, DTO.Course>().ReverseMap();
        }
    }
}