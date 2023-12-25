using System.Data;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain.DTOs;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
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
    public async Task<ActionResult<List<PosterDto>>> GetPosters([FromQuery] int pageSize = 10, [FromQuery] int pageCount = 1)
    {
        return _context.Posters.Skip(pageSize * pageCount).Take(pageSize)
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
}