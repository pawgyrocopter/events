using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PizzaApp.DTOs;
using PizzaApp.Entities;
using PizzaApp.Interfaces;

namespace PizzaApp.Data;

public class PizzaRepository : IPizzaRepository
{
    private readonly DataContext _context;
    private readonly IMapper _mapper;
    private readonly IPhotoService _photoService;

    public PizzaRepository(DataContext context, IMapper mapper, IPhotoService photoService)
    {
        _context = context;
        _mapper = mapper;
        _photoService = photoService;
    }

    public async Task<IEnumerable<PizzaDto>> GetPizzas()
    {
        return await _context.Pizzas
            .Include(p => p.Photo)
            .ProjectTo<PizzaDto>(_mapper.ConfigurationProvider)
            .ToListAsync();
    }


    public async Task<ActionResult<PizzaDto>> GetPizza(string name)
    {
        var pizza = await _context.Pizzas
            .Include(p => p.Photo)
            .ProjectTo<PizzaDto>(_mapper.ConfigurationProvider).FirstOrDefaultAsync(x => x.Name == name);
        pizza.Topings = new List<TopingDto>();
        return pizza;
    }

    public async Task<ActionResult<Pizza>> AddPizza([FromBody]IFormFile file, PizzaDto pizzaDto)
    {
        var result = await _photoService.AddPhotoAsync(file);

       // if (result.Error != null) return BadRequest(result.Error.Message);

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
        
        return pizza;
    }
}