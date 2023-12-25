﻿using AutoMapper;
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

    [HttpGet("{userId:guid}/events")]
    public async Task<ActionResult<List<EventDto>>> GetEventsByUserId(Guid userId)
    {
        var user = await _context.Users.Include(x => x.Events)
            .FirstOrDefaultAsync(x => x.Id == userId);
        if (user is null)
            return NotFound("Current user douesn't exist");

        return _mapper.Map<List<EventDto>>(user.Events);
    }
    
    [HttpGet("{email}/events")]
    public async Task<ActionResult<List<EventDto>>> GetEventsByUserEmail(string email)
    {
        var user = await _context.Users.Include(x => x.Events)
            .FirstOrDefaultAsync(x => x.Email == email);
        if (user is null)
            return NotFound("Current user douesn't exist");

        return  _mapper.Map<List<EventDto>>(user.Events);
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

    [HttpPut("{userId}")]
    public async Task<ActionResult> UpdateUser([FromBody] UserUpdateDto updateUser, Guid userId)
    {
        var user = await _userManager.Users.AsNoTracking().Include(x => x.Photos).FirstOrDefaultAsync(x => x.Id == userId);

        if (user is null)
            return NotFound("No such user");
        
        user.UserName = updateUser.Name ?? user.UserName;
        user.Email = updateUser.Email ?? user.Email;
        user.Photos ??= new List<Photo>();
        if (updateUser.Photos is not null)
            user.Photos.AddRange(updateUser.Photos.Select(x => new Photo(x)));
        
        // await _userManager.UpdateNormalizedEmailAsync(user);
        // await _userManager.UpdateNormalizedUserNameAsync(user);
        var result = await _userManager.UpdateAsync(user);
        // await _context.SaveChangesAsync();
        // await _context.SaveChangesAsync();

        return Ok(result);
    }
}