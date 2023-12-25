using System.Security.Claims;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain.DTOs;
using Domain.DTOs.Events;
using Domain.DTOs.User;
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
    private readonly IMapper _mapper;

    public UsersController(UserManager<User> userManager, DataContext context, IMapper mapper)
    {
        _userManager = userManager;
        _context = context;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<List<UserGetDto>>> GetUsers()
    {
        return _context.Users.ProjectTo<UserGetDto>(_mapper.ConfigurationProvider).ToList();
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

    [HttpGet("events")]
    public async Task<ActionResult<List<EventDto>>> GetUserEvents()
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        var user = await _context.Users.Include(x => x.Events)
            .FirstOrDefaultAsync(x => x.Id == Guid.Parse(userId));
        
        if (user is null)
            return NotFound("Current user doesn't exist");

        return _mapper.Map<List<EventDto>>(user.Events);
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

    [HttpPut]
    [Authorize]
    public async Task<ActionResult> UpdateUser([FromBody] UserUpdateDto updateUser)
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (!Guid.TryParse(userId, out var id))
            return BadRequest("Incorrect token");

        var user = await _context.Users
            .Include(x => x.Photos)
            .Include(x => x.Events)
            .FirstOrDefaultAsync(x => x.Id == id);
        
        if (user is null)
            return NotFound("No such user");
        
        user.UserName = updateUser.Name ?? user.UserName;
        user.Email = updateUser.Email ?? user.Email;
        user.Photos ??= new List<Photo>();
        if (updateUser.Photos is not null)
            user.Photos.AddRange(updateUser.Photos.Select(x => new Photo(x)));
        
        var result = await _userManager.UpdateAsync(user);

        return Ok(result);
    }
}