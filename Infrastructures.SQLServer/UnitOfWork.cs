using AutoMapper;
using Infrastructures.SQLServer.DataContext;
using Microsoft.EntityFrameworkCore;
using UseCase.Repositories;
using UseCases.GenericRepository;
using UseCases.Repositories;
using UseCases.UnitOfWork;
using Course = Entities.Course;
using Student = Entities.Student;

namespace Infrastructures.SQLServer;

public class UnitOfWork: IUnitOfWork
{
    private readonly SchoolContext _context;
    private readonly IMapper _mapper;

    public IStudentRepository StudentsRepository { get; }
    public IUserRepository UsersRepository { get; }

    public UnitOfWork(SchoolContext context, IStudentRepository studentRepository
        , IUserRepository userRepository
        , IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
        StudentsRepository = studentRepository;
        UsersRepository = userRepository;
    }


    public async Task<int> SaveAsync()
    {
        return await _context.SaveChangesAsync();
    }
    
}