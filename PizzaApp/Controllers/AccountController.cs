using Domain.DTOs;
using Domain.Interfaces.IServices;
using Microsoft.AspNetCore.Mvc;

namespace PizzaApp.Controllers;

public class AccountController : BaseApiController
{
    private readonly IAccountService _accountService;

    public AccountController(IAccountService accountService)
    {
        _accountService = accountService;
    }
    
    [HttpPost("register")]
    public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto)
    {
        //try catch for errors
        return await _accountService.Register(registerDto);
    }

    [HttpPost("login")]
    public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
    {
        //try catch for errors
        return await _accountService.Login(loginDto);
    }
}