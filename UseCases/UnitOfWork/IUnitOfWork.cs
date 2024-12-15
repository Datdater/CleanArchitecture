using Entities;
using UseCases.GenericRepository;

namespace UseCases.UnitOfWork;

public interface IUnitOfWork 
{
    // Example: Define repositories as properties
    IGenericRepository<Student> Students { get; }
    IGenericRepository<Course> Courses { get; }

    // Commit changes to the database
    Task<int> SaveAsync();
}