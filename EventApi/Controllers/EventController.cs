using System.Security.Claims;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain.DTOs.Events;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.AspNetCore.Authorization;
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
    public async Task<ActionResult<List<EventDto>>> GetEvents([FromQuery] DateTime? fromTime = null, [FromQuery] DateTime? toTime = null)
    {
        var from = fromTime ?? DateTime.UtcNow.AddDays(-30); 
        var to = toTime ?? DateTime.UtcNow; 
        
        return _context.Events.OrderBy(x => x.To).Where(x => x.From >= from && x.To <= to).Select(x => x).ProjectTo<EventDto>(_mapper.ConfigurationProvider).ToList();
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

        eventModel.From = eventDto.From ?? eventModel.From;
        eventModel.To = eventDto.To ?? eventModel.To;
        eventModel.Address = eventDto.Address ?? eventModel.Address;
        eventModel.Name = eventDto.Name ?? eventModel.Name;
        eventModel.Description = eventDto.Description ?? eventModel.Description;
        eventModel.ShortDescription = eventDto.ShortDescription ?? eventModel.ShortDescription;

        if (eventDto.UsersToRemove is not null)
            foreach (var userEventUpdateDto in eventDto.UsersToRemove)
            {
                var user = await _userManager.FindByIdAsync(userEventUpdateDto.Id.ToString());
                if (user is not null)
                    eventModel.Users.Remove(user);
            }
        if (eventDto.UsersToAdd is not null)
            foreach (var userEventUpdateDto in eventDto?.UsersToAdd)
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
    [Authorize]
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
    
    [HttpDelete("{eventId:guid}")]
    [Authorize]
    public async Task<ActionResult<Event>> CreateEvent(Guid eventId)
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (!Guid.TryParse(userId, out var id))
            return BadRequest("Incorrect token");

        var eventModel = await _context.Events.FirstOrDefaultAsync(x => x.Id == eventId);

        if (eventModel is null)
            return BadRequest("No such Event");
        
        if (eventModel.CreatorId != id)
            return Unauthorized();
        
        try
        {
            _context.Events.Remove(eventModel);
            await _context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }

        return Ok("Event deleted");
    }

    
    [Authorize]
    [HttpPut("like/{eventId:guid}")]
    public async Task<ActionResult> Like(Guid eventId)
    {
        var eventModel = await _context.Events.FirstOrDefaultAsync(x => x.Id == eventId);

        if (eventModel is null)
            return NotFound();
        
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        
        if (!Guid.TryParse(userId, out var id))
            return BadRequest("Incorrect token");

        var user = await _context.Users.Include(x => x.LikedEvents).FirstOrDefaultAsync(x => x.Id == id);

        if (user is null)
            return NotFound("User not found");

        try
        {
            user.LikedEvents.Add(eventModel);
            await _context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
        
        return Ok();
    }
    
    [Authorize]
    [HttpPut("dislike/{eventId:guid}")]
    public async Task<ActionResult> DisLike(Guid eventId)
    {
        var eventModel = await _context.Events.FirstOrDefaultAsync(x => x.Id == eventId);

        if (eventModel is null)
            return NotFound();
        
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        
        if (!Guid.TryParse(userId, out var id))
            return BadRequest("Incorrect token");

        var user = await _context.Users.Include(x => x.LikedEvents).FirstOrDefaultAsync(x => x.Id == id);

        if (user is null)
            return NotFound("User not found");

        try
        {
            user.LikedEvents.Remove(eventModel);
            await _context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
        
        return Ok();
    }
}