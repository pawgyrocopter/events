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

    public EventController(DataContext context, UserManager<User> userManager)
    {
        _context = context;
        _userManager = userManager;
    }


  
    [HttpPut("{eventId:guid}")]
    public async Task<ActionResult<Event>> UpdateEvent([FromBody] EventDto eventDto, Guid eventId)
    {
        var eventModel = await _context.Events.FirstOrDefaultAsync(x => x.Id == eventId);

        if (eventModel is null)
            return NotFound("No such event with id" + eventId);

        eventModel.Date = eventDto.Date ?? eventModel.Date;
        eventModel.Address = eventDto.Address ?? eventModel.Address;
        eventModel.Name = eventDto.Name ?? eventModel.Name;
        eventModel.Description = eventDto.Description ?? eventModel.Description;
        eventModel.ShortDescription = eventDto.ShortDescription ?? eventModel.ShortDescription;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }

        return Ok(new EventDto(eventModel));
    }
    
    [HttpPost]
    public async Task<ActionResult<Event>> CreateEvent([FromBody] EventDto eventDto)
    {
        var eventModel = new Event(eventDto);
        try
        {
            await _context.Events.AddAsync(eventModel);
            await _context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }

        return Ok(new EventDto(eventModel));
    }
}