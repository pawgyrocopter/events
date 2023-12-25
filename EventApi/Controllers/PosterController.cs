using System.Data;
using System.Security.Claims;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain.DTOs;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SQLitePCL;

namespace PizzaApp.Controllers;

public class PosterController : BaseApiController
{
    private readonly IMapper _mapper;
    private readonly DataContext _context;

    public PosterController(IMapper mapper, DataContext context)
    {
        _mapper = mapper;
        _context = context;
    }

    
    [HttpGet]
    public async Task<ActionResult<List<PosterDto>>> GetPosters([FromQuery] DateTime? fromTime = null, [FromQuery] DateTime? toTime = null,
        [FromQuery] int pageSize = 10, [FromQuery] int pageCount = 0)
    {
        var from = fromTime ?? DateTime.UtcNow.AddDays(-30); 
        var to = toTime ?? DateTime.UtcNow; 
        
        return _context.Posters.OrderBy(x => x.To).Where(x => x.From >= from && x.To <= to).Select(x => x).Skip(pageSize * pageCount).Take(pageSize)
            .ProjectTo<PosterDto>(_mapper.ConfigurationProvider).ToList();
    }

    [HttpPost]
    public async Task<ActionResult<PosterDto>> CreatePoster([FromBody] PosterCreateDto dto)
    {
        var poster = new Poster(dto);

        try
        {
            _context.Posters.Add(poster);
            await _context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }

        return _mapper.Map<Poster, PosterDto>(poster);
    }
    
    [Authorize]
    [HttpPut("like/{posterId:guid}")]
    public async Task<ActionResult> Like(Guid posterId)
    {
        var poster = await _context.Posters.FirstOrDefaultAsync(x => x.Id == posterId);

        if (poster is null)
            return NotFound();
        
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        
        if (!Guid.TryParse(userId, out var id))
            return BadRequest("Incorrect token");

        var user = await _context.Users.Include(x => x.LikedPosters).FirstOrDefaultAsync(x => x.Id == id);

        if (user is null)
            return NotFound("User not found");

        try
        {
            user.LikedPosters.Add(poster);
            await _context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
        
        return Ok();
    }
    
    [Authorize]
    [HttpPut("dislike/{posterId:guid}")]
    public async Task<ActionResult> DisLike(Guid posterId)
    {
        var poster = await _context.Posters.FirstOrDefaultAsync(x => x.Id == posterId);

        if (poster is null)
            return NotFound();
        
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        
        if (!Guid.TryParse(userId, out var id))
            return BadRequest("Incorrect token");

        var user = await _context.Users.Include(x => x.LikedPosters).FirstOrDefaultAsync(x => x.Id == id);

        if (user is null)
            return NotFound("User not found");

        try
        {
            user.LikedPosters.Remove(poster);
            await _context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
        
        return Ok();
    }
}