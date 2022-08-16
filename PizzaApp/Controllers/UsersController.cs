using Domain.Entities;
using Domain.Interfaces.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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