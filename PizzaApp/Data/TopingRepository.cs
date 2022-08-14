using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using PizzaApp.DTOs;
using PizzaApp.Entities;
using PizzaApp.Interfaces;

namespace PizzaApp.Data;

public class TopingRepository : ITopingRepository
{
    private readonly DataContext _context;
    private readonly IMapper _mapper;

    public TopingRepository(DataContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<IQueryable<Toping>> GetTopings()
    {
        return _context.Topings.AsQueryable();
    }

    public async Task<Toping> CreateToping(string name)
    {
        var toping = new Toping() {Name = name};
        await _context.Topings.AddAsync(toping);
        return toping;
    }

    public async Task<Toping> GetTopingById(int topingId)
    {
        return await _context.Topings.FirstOrDefaultAsync(x => x.Id == topingId);
    }
}