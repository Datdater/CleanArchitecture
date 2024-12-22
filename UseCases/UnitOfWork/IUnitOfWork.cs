using Entities;
using UseCase.Repositories;
using UseCases.GenericRepository;
using UseCases.Repositories;

namespace UseCases.UnitOfWork;

public interface IUnitOfWork 
{
    // Example: Define repositories as properties
    IStudentRepository StudentsRepository { get; }
    IUserRepository UsersRepository { get; }
    // Commit changes to the database
    Task<int> SaveAsync();
}