using Microsoft.AspNetCore.Mvc;
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
        return null;
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
            Weight = pizzaDto.Weight
        };
        _context.Pizzas.Add(pizza);
        _context.Photos.Add(photo);

        await _context.SaveChangesAsync();
        return pizza;
    }
}