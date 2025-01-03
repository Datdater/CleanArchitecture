﻿using API.Controller;
using API.Models;
using AutoMapper;
using Entities;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using UseCase;
using UseCase.Commons;
using UseCase.ViewModels.UserViewModels;
using UseCases.UnitOfWork;


public class UserController(IUserService userService, IMapper mapper) : BaseController
{
    [HttpPost]
    public async Task<LoginResponse> Login([FromBody] LoginRequest loginRequest)
    {
        var loginResponse = await userService.LoginAsync(loginRequest);
        return loginResponse;
    }
    [HttpPost]
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

    [HttpPost]
    public async Task<TokenResponse> RefreshToken([FromBody] TokenRequest model)
    {
        return await userService.RefreshTokenAsync(model);
    }
}