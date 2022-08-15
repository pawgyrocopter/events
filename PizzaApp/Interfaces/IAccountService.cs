using Microsoft.AspNetCore.Mvc;
using PizzaApp.DTOs;

namespace PizzaApp.Interfaces;

public interface IAccountService
{
    Task<ActionResult<UserDto>> Register(RegisterDto registerDto);
    Task<ActionResult<UserDto>> Login(LoginDto loginDto);
}