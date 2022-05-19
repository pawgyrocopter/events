using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PizzaApp.Data;
using PizzaApp.Entities;

namespace PizzaApp.Controllers;

public class TopingsController : BaseApiController
{
    private readonly DataContext _context;

    public TopingsController(DataContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Toping>>> GetTopings()
    {
        return await _context.Topings.ToListAsync();
    }
}