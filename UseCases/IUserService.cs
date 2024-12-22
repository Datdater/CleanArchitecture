using Entities;
using UseCase.ViewModels.UserViewModels;

namespace UseCase;

public interface IUserService
{ 
    Task<User> GetUserByIdAsync(int id);
    Task<User> GetUserByUsernameAsync(string username);
    Task<LoginResponse> LoginAsync(LoginRequest loginRequest);
    
    Task<bool> LogoutAsync(string username);
}