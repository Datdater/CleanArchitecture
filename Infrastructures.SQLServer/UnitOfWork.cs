using AutoMapper;
using Infrastructure;
using Infrastructure.DataContext;
using Infrastructures.SQLServer.DataContext;
using Microsoft.EntityFrameworkCore;
using UseCases.GenericRepository;
using UseCases.UnitOfWork;
using Course = Entities.Course;
using Student = Entities.Student;

namespace Infrastructures.SQLServer;

public class UnitOfWork: IUnitOfWork
{
    private readonly SchoolContext _context;
    private readonly IMapper _mapper;

    public IGenericRepository<Student> Students { get; }
    public IGenericRepository<Course> Courses { get; }
    public UnitOfWork(SchoolContext context)
    {
        _context = context;

        Students = new GenericRepository<Student, StudentEntity>(_context, _mapper);
        Courses = new GenericRepository<Course, CourseEntity>(_context, _mapper);
    }
    public async Task<int> SaveAsync()
    {
        return await _context.SaveChangesAsync();
    }
    
}