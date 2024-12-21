using AutoMapper;
using Entities;
using Infrastructures.SQLServer.DataContext;
using UseCase.Repositories;

namespace Infrastructures.SQLServer.Repositories;

public class StudentRepository: GenericRepository<Student, StudentEntity>, IStudentRepository
{
    public StudentRepository(SchoolContext context, IMapper mapper) : base(context, mapper)
    {
    
    }
    
}
