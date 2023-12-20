using Domain.DTOs;
using Domain.Entities;
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
        UserDto userDto;
        try
        {
            userDto = await _accountService.Register(registerDto);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }

        return userDto;
    }

    [HttpPost("login")]
    public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
    {
        UserDto userDto;
        try
        {
            userDto =  await _accountService.Login(loginDto);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }

        return userDto;
    }
}