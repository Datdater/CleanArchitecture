using API.Models;
using AutoMapper;
using Entities;
using UseCase.Commons;

namespace API.Mapper;

public class MapUIModel : Profile
{
    public  MapUIModel()
    {
        CreateMap(typeof(Pagination<>), typeof(Pagination<>)); // Generic pagination support
        CreateMap<Student, StudentModel>();
    }
}