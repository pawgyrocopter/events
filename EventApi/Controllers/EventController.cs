using System.Security.Claims;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain.DTOs.Events;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace PizzaApp.Controllers;

public class EventController : BaseApiController
{
    private readonly DataContext _context;
    private readonly UserManager<User> _userManager;
    private readonly IMapper _mapper;

    public EventController(DataContext context, UserManager<User> userManager, IMapper mapper)
    {
        _context = context;
        _userManager = userManager;
        _mapper = mapper;
    }


    [HttpGet]
    public async Task<ActionResult<List<EventDto>>> GetEvents()
    {
        return _context.Events.ProjectTo<EventDto>(_mapper.ConfigurationProvider).ToList();
    }

    [HttpGet("{eventId:guid}")]
    public async Task<ActionResult<EventDto>> GetEvent(Guid eventId)
    {
        var eventModel = await _context.Events.Include(x => x.Users).FirstOrDefaultAsync(x => x.Id == eventId);
        return _mapper.Map<EventDto>(eventModel);
    }
    
    
    [HttpPut("{eventId:guid}")]
    public async Task<ActionResult<Event>> UpdateEvent([FromBody] EventUpdateDto eventDto, Guid eventId)
    {
        var eventModel = await _context.Events.Include(x => x.Users).FirstOrDefaultAsync(x => x.Id == eventId);

        if (eventModel is null)
            return NotFound("No such event with id" + eventId);

        eventModel.Date = eventDto.Date;
        eventModel.Address = eventDto.Address ?? eventModel.Address;
        eventModel.Name = eventDto.Name ?? eventModel.Name;
        eventModel.Description = eventDto.Description ?? eventModel.Description;
        eventModel.ShortDescription = eventDto.ShortDescription ?? eventModel.ShortDescription;

        foreach (var userEventUpdateDto in eventDto.UsersToRemove)
        {
            var user = await _userManager.FindByIdAsync(userEventUpdateDto.Id.ToString());
            if (user is not null)
                eventModel.Users.Remove(user);
        }
        
        foreach (var userEventUpdateDto in eventDto.UsersToAdd)
        {
            var user = await _userManager.FindByIdAsync(userEventUpdateDto.Id.ToString());
            if (user is not null)
                eventModel.Users.Add(user);
        }
        
        try
        {
            await _context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }

        return Ok(_mapper.Map<Event, EventDto>(eventModel));
    }
    
    [HttpPost]
    public async Task<ActionResult<Event>> CreateEvent([FromBody] EventCreateDto eventDto)
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (!Guid.TryParse(userId, out var id))
            return BadRequest("Incorrect token");

        var user = await _userManager.FindByIdAsync(userId);
        if (user is null)
            return BadRequest("No such user");
        
        var eventModel = new Event(eventDto, user);
        try
        {
            await _context.Events.AddAsync(eventModel);
            await _context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }

        return Ok(_mapper.Map<Event, EventDto>(eventModel));
    }
}