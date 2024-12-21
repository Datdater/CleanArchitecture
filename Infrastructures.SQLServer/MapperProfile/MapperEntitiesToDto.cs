using AutoMapper;
using Entities;
using Infrastructures.SQLServer.DataContext;
using UseCase.Commons;

namespace Infrastructures.MapperProfile;

public class MapperEntitiesToDto: AutoMapper.Profile
{
    public MapperEntitiesToDto()
    {
        CreateMap<Student, StudentEntity>().ReverseMap();
        CreateMap<Infrastructures.SQLServer.DataContext.StudentEntity, Entities.Student>().ReverseMap();
        CreateMap(typeof(Pagination<>), typeof(Pagination<>)); // Generic pagination support
    }
}