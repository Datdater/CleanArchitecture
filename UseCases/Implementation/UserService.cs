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
        await cacheService.SetAsync($"accessToken:{accessToken}", user.Id + "", TimeSpan.FromMinutes(15));
        await cacheService.SetAsync(user.Id + "", $"refreshToken:{refreshToken}", TimeSpan.FromDays(7));
        return new LoginResponse 
        {
            UserId = user.Id + "",
            AccessToken = accessToken,
            RefreshToken = refreshToken
        };
    }

    public async Task<TokenResponse> RefreshTokenAsync(TokenRequest tokenRequest)
    {
        var userId = await cacheService.GetAsync<string>($"accessToken:{tokenRequest.AccessToken}");
        var refreshToken = await cacheService.GetAsync<string>(userId);
        if (refreshToken != "refreshToken:" + tokenRequest.RefreshToken)
        {
            throw new UnAuthenticationException("Invalid Refresh Token");
        }

        var user = await unitOfWork.UsersRepository.GetByIdAsync(int.Parse(userId));
        //delete key
        await cacheService.RemoveAsync($"accessToken:{tokenRequest.AccessToken}");
        await cacheService.RemoveAsync($"{userId}");
        //generate access and refresh token
        var newAccessToken = TokenUtils.GenerateAccessToken(user, configuration.JWTSecretKey);
        var newRefreshToken = TokenUtils.GenerateRefreshToken();
        //Set key to redis cache
        await cacheService.SetAsync($"accessToken:{newAccessToken}", user.Id + "", TimeSpan.FromMinutes(15));
        await cacheService.SetAsync(user.Id + "", $"refreshToken:{newRefreshToken}", TimeSpan.FromDays(7));
        var response = new TokenResponse
        {
            AccessToken = newAccessToken,
            RefreshToken =  newRefreshToken// Return the same refresh token
        };
        return response;
    }
    
    public async Task<bool> LogoutAsync(string userId)
    {
        var user = await unitOfWork.UsersRepository.GetByIdAsync(int.Parse(userId)) ?? throw new ValidationException("User not found");
        
        var refreshToken = await cacheService.GetAsync<string>(userId);
        if (refreshToken == null)
        {
            return false;
        }
        return await cacheService.RemoveAsync(userId);

    }
}