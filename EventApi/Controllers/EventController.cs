using EventApi.DTOs;
using EventApi.Models;
using EventDatabase;
using EventDatabase.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EventApi.Controllers;

public class EventController : BaseController
{
    private readonly ApplicationDbContext _context;
    
    public EventController(ApplicationDbContext context, ILogger<EventController> logger) : base(logger)
    {
        _context = context;
    }

    
    [HttpGet]
    public ActionResult<List<EventModel>> GetEvents()
    {
        return _context.Events.AsNoTracking().Select(x => new EventModel(x)).ToList();
    }

    [HttpGet("{id:guid}")]
    public ActionResult<EventEntity?> GetEvent(Guid id)
    {
        return _context.Events.FirstOrDefault(x => x.Id == id);
    }
    
    [HttpPut("{id:guid}")]
    public ActionResult<EventEntity?> UpdateEvent(Guid id)
    {
        return _context.Events.FirstOrDefault(x => x.Id == id);
    }

    [HttpPost]
    public ActionResult<EventModel> CreateEvent([FromBody] EventDto eventDto)
    {
        var entity = new EventEntity(Guid.NewGuid(), eventDto.Name);
        
        _context.Events.Add(entity);
        _context.SaveChanges();

        return new EventModel(entity);
    }
}