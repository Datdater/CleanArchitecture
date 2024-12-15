using AutoMapper;
using Entities;
using Infrastructure.DataContext;

namespace Infrastructure.MapperProfile;

public class MapperEntitiesToDto: AutoMapper.Profile
{
    public MapperEntitiesToDto()
    {
        CreateMap<Course, CourseEntity >();
        CreateMap<Student, StudentEntity>();
    }
}