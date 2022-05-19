using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PizzaApp.Data;
using PizzaApp.DTOs;
using PizzaApp.Entities;
using PizzaApp.Interfaces;

namespace PizzaApp.Controllers;

public class PizzasController : BaseApiController
{
    private readonly DataContext _context;
    private readonly IPhotoService _photoService;

    public PizzasController(DataContext context, IPhotoService photoService)
    {
        _context = context;
        _photoService = photoService;
    }

    [HttpGet]
    public async Task<IEnumerable<Pizza>> GetPizzas()
    {
        return await _context.Pizzas
            .Include(p => p.Photo)
            .ToListAsync();
    }

    [HttpGet("{name}", Name = "GetPizza")]
    public async Task<ActionResult<Pizza>> GetPizza(string name)
    {
        return await _context.Pizzas.Include(p => p.Photo).FirstOrDefaultAsync(x => x.Name == name);
    }

    [HttpPost("add-pizza")]
    public async Task<ActionResult<Pizza>> AddPizza(IFormFile file, [FromQuery]PizzaDto pizzaDto)
    {
        var result = await _photoService.AddPhotoAsync(file);

        if (result.Error != null) return BadRequest(result.Error.Message);

        var photo = new Photo()
        {
            Url = result.SecureUrl.AbsoluteUri,
            PublicId = result.PublicId
        };
        var pizza = new Pizza()
        {
            Name = pizzaDto.Name,
            Photo = photo,
            Cost = pizzaDto.Cost,
            Ingredients = pizzaDto.Ingredients,
            Weight = pizzaDto.Weight,
            State = State.Pending,
        };
        _context.Pizzas.Add(pizza);
        _context.Photos.Add(photo);

        await _context.SaveChangesAsync();
        return pizza;
    }
}