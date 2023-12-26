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
    public async Task<ActionResult<UserGetDto>> GetUserByUserId(Guid userId)
    {
        var user = await _context.Users.Include(x => x.Events)
            .FirstOrDefaultAsync(x => x.Id == userId);
        if (user is null)
            return NotFound("Current user douesn't exist");

        return _mapper.Map<UserGetDto>(user);
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
    public async Task<ActionResult<UserGetDto>> UpdateUser([FromBody] UserUpdateDto updateUser)
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        var user = await _context.Users
            .Include(x => x.Photos)
            .Include(x => x.Events)
            .FirstOrDefaultAsync(x => x.Id == Guid.Parse(userId));
        
        if (user is null)
            return NotFound("No such user");

        if (await _userManager.FindByNameAsync(updateUser.UserName ?? string.Empty) is null)
            user.UserName = updateUser.UserName ?? user.UserName;
        if (await _userManager.FindByEmailAsync(updateUser.Email ?? string.Empty) is null)
            user.Email = updateUser.Email ?? user.Email;

        user.FirstName = updateUser.FirstName ?? user.FirstName;
        user.SecondName = updateUser.SecondName ?? user.SecondName;
        user.Age = updateUser.Age ?? user.Age;
        user.ShortDescription = updateUser.ShortDescription ?? user.ShortDescription;
        user.Interests = updateUser.Interests ?? user.Interests;
        user.TelegramLink = updateUser.TelegramLink ?? user.TelegramLink;
        user.VKLink = updateUser.VKLink ?? user.VKLink;
        user.Base64Photo = updateUser.Base64Photo ?? user.Base64Photo;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
      
       
        return Ok(_mapper.Map<UserGetDto>(user));
        // while (true)
        // {
        //     var result = await _userManager.UpdateAsync(user);
        //     if (result.Succeeded)
        //         break;
        // }
        // try
        // {
        //     await _context.SaveChangesAsync();
        // }
        // catch (Exception ex)
        // {
        //     var a = 1;
        // }
        
    }
}