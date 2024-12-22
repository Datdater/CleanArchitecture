using API.Models;
using AutoMapper;
using Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using UseCase;
using UseCase.Commons;
using UseCases.UnitOfWork;

namespace API.Controller;

public class StudentController: BaseController
{
    private readonly IManageStudent _manageStudent;
    private readonly IMapper _mapper;
    public StudentController(IManageStudent manageStudent, IMapper mapper)
    {
        _manageStudent = manageStudent;
        _mapper = mapper;
    }
    
    [HttpGet]
    [Authorize]
    public async Task<Pagination<StudentModel>> GetAllStudents( [FromQuery]int pageIndex,[FromQuery] int pageSize)
    {
        var students = await _manageStudent.GetAllStudentsAsync(pageIndex, pageSize);
        return _mapper.Map<Pagination<StudentModel>>(students);
    }
    
}