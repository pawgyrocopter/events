using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PizzaApp.Data;
using PizzaApp.Entities;
using PizzaApp.Interfaces;

namespace PizzaApp.Controllers;

public class UsersController : BaseApiController
{
    private readonly IUserService _userService;

    public UsersController(IUserService userService )
    {
        _userService = userService;
    }

    [HttpGet]
    [Authorize]
    public async Task<IEnumerable<User>> GetUsers()
    {
        return await _userService.GetUsers();
    }
}