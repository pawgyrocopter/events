using Domain.Entities;
using Domain.Interfaces.IServices;
using Infrastructure.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace PizzaApp.Controllers;

public class UsersController : BaseApiController
{
    private readonly UserManager<User> _userManager;
    private readonly DataContext _context;

    public UsersController(UserManager<User> userManager, DataContext context)
    {
        _userManager = userManager;
        _context = context;
    }


    [HttpGet("{userId:guid}")]
    public async Task<ActionResult<User>> GetUserByUserId(Guid userId)
    {
        var user = await _context.Users.Include(x => x.Events)
            .FirstOrDefaultAsync(x => x.Id == userId);
        if (user is null)
            return NotFound("Current user douesn't exist");

        return user;
    }

    [HttpGet("{userId:guid}/events")]
    public async Task<ActionResult<List<Event>>> GetEventsByUserId(Guid userId)
    {
        var user = await _context.Users.Include(x => x.Events)
            .FirstOrDefaultAsync(x => x.Id == userId);
        if (user is null)
            return NotFound("Current user douesn't exist");

        return user.Events;
    }
    
    [HttpGet("{email}/events")]
    public async Task<ActionResult<List<Event>>> GetEventsByUserEmail(string email)
    {
        var user = await _context.Users.Include(x => x.Events)
            .FirstOrDefaultAsync(x => x.Email == email);
        if (user is null)
            return NotFound("Current user douesn't exist");

        return user.Events;
    }

    [HttpGet("{email}")]
    public async Task<ActionResult<User>> GetUserByEmail(string email)
    {
        var user = await _context.Users.Include(x => x.Events)
            .FirstOrDefaultAsync(x => x.Email == email);
        if (user is null)
            return NotFound("Current user douesn't exist");

        return user;
    }
}