using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using AutoMapper;
using Entities;
using Microsoft.IdentityModel.Tokens;
using UseCase;
using UseCase.Commons;
using UseCase.Utils;
using UseCase.ViewModels.UserViewModels;
using UseCases.Commons;
using UseCases.UnitOfWork;

namespace UseCases.Implementation;

public class UserService(IUnitOfWork unitOfWork, IMapper mapper, AppConfiguration configuration, ICacheService cacheService): IUserService
{
    
    public Task<User> GetUserByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<User> GetUserByUsernameAsync(string username)
    {
        throw new NotImplementedException();
    }

    public async Task<LoginResponse> LoginAsync(LoginRequest loginRequest)
    {
        var user = await unitOfWork.UsersRepository.FindUserByUsernameAndPassword(loginRequest.Username, loginRequest.Password);
        if (user == null)
        {
            return null;
        }
        var accessToken = TokenUtils.GenerateAccessToken(user, configuration.JWTSecretKey);
        var refreshToken = TokenUtils.GenerateRefreshToken();
        await cacheService.SetAsync($"accessToken:{accessToken}", user.Username, TimeSpan.FromMinutes(15));
        await cacheService.SetAsync(user.Username, $"refreshToken:{refreshToken}", TimeSpan.FromDays(7));
        return new LoginResponse 
        {
            Username = user.Username,
            AccessToken = accessToken,
            RefreshToken = refreshToken
        };
    }

    public async Task<bool> LogoutAsync(string userId)
    {
        var user = await unitOfWork.UsersRepository.GetByIdAsync(int.Parse(userId)) ?? throw new ValidationException("User not found");
        
        var refreshToken = await cacheService.GetAsync<string>(user.Username);
        if (refreshToken == null)
        {
            return false;
        }
        return await cacheService.RemoveAsync(user.Username);

    }
}