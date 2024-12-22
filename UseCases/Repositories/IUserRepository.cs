using Entities;
using UseCases.GenericRepository;

namespace UseCases.Repositories;

public interface IUserRepository: IGenericRepository<User>
{
    Task<User> FindUserByUsernameAndPassword(string username, string password);
}