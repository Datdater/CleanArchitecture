using Entities;
using UseCase.Repositories;
using UseCases.GenericRepository;

namespace UseCases.UnitOfWork;

public interface IUnitOfWork 
{
    // Example: Define repositories as properties
    IStudentRepository StudentsRepository { get; }

    // Commit changes to the database
    Task<int> SaveAsync();
}