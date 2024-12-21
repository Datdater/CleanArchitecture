using AutoMapper;
using Infrastructures.SQLServer.DataContext;
using Microsoft.EntityFrameworkCore;
using UseCase.Repositories;
using UseCases.GenericRepository;
using UseCases.UnitOfWork;
using Course = Entities.Course;
using Student = Entities.Student;

namespace Infrastructures.SQLServer;

public class UnitOfWork: IUnitOfWork
{
    private readonly SchoolContext _context;
    private readonly IMapper _mapper;

    public IStudentRepository StudentsRepository { get; }

    public UnitOfWork(SchoolContext context, IStudentRepository studentRepository, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
        StudentsRepository = studentRepository;
    }


    public async Task<int> SaveAsync()
    {
        return await _context.SaveChangesAsync();
    }
    
}