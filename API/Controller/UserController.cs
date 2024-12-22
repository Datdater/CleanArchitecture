using API.Models;
using AutoMapper;
using Entities;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using UseCase;
using UseCase.Commons;
using UseCase.ViewModels.UserViewModels;
using UseCases.UnitOfWork;

[Route("api/[controller]")]
[ApiController]
public class UserController(IUserService userService, IMapper mapper) 
{
    [HttpPost("login")]
    public async Task<LoginResponse> Login([FromBody] LoginRequest loginRequest)
    {
        var loginResponse = await userService.LoginAsync(loginRequest);
        return loginResponse;
    }
    [HttpPost("logout")]
    public async Task<string> Logout([FromBody] LogoutRequest model)
    {
        
        var result = await userService.LogoutAsync(model.UserId);
        if (result)
        {
            return "Logout successfully";
        }
        else
        {
            return "Logout failed";
        }
    }
}