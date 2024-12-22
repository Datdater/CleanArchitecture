using AutoMapper;
using Entities;
using Infrastructures.SQLServer.DataContext;
using Infrastructures.SQLServer.Repositories;
using UseCase.Commons;

namespace Infrastructures.MapperProfile;

public class MapperEntitiesToDto: AutoMapper.Profile
{
    public MapperEntitiesToDto()
    {
        CreateMap<Student, StudentEntity>().ReverseMap();
        CreateMap<User, UserEntity>().ReverseMap();
        CreateMap(typeof(Pagination<>), typeof(Pagination<>)); // Generic pagination support
    }
}