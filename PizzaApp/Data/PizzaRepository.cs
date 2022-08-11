using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PizzaApp.DTOs;
using PizzaApp.Entities;
using PizzaApp.Interfaces;
using SQLitePCL;

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

    public async Task<Pizza> GetPizzaByName(string pizzaName)
    {
        return await _context.Pizzas.FirstOrDefaultAsync(x => x.Name.Equals(pizzaName));
    }

    public async Task<ActionResult<Pizza>> AddPizza([FromBody] IFormFile file, PizzaDto pizzaDto)
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

    public async Task<ActionResult<PizzaDto>> UpdatePizza(PizzaDto pizzaDto)
    {
        var pizza = await _context.Pizzas.FirstOrDefaultAsync(x => x.Name == pizzaDto.Name);
        pizza.Cost = pizzaDto.Cost;
        pizza.Ingredients = pizzaDto.Ingredients;
        pizza.Weight = pizzaDto.Weight;
        _context.Pizzas.Update(pizza);
        return _mapper.Map<PizzaDto>(pizza);
    }

    public async Task<PizzaDto> UpdatePizzaOrderState(int pizzaId,int state)
    {
        var pizza = await _context.PizzaOrders.Include(x => x.Order).ThenInclude(d => d.Pizzas)
            .FirstOrDefaultAsync(x => x.Id == pizzaId);
        var order = await _context.Orders.Include(p => p.Pizzas).FirstOrDefaultAsync(x => x.Id == pizza.OrderId);
        if (pizza == null) return null;
        pizza.State = state switch
        {
            0 => State.Pending,
            1 => State.InProgress,
            2 => State.Ready,
            _ => State.Canceled
        };
        bool check = pizza.Order.Pizzas.All(x => x.State == State.Ready);
        if (check)
        {
            pizza.Order.OrderState = OrderState.Ready;
        }
        return _mapper.Map<PizzaDto>(pizza);
    }

    public async Task<IEnumerable<PizzaDto>> GetPizzasByOrderId(int orderId)
    {
        return _context.PizzaOrders.Where(x => x.OrderId == orderId).ProjectTo<PizzaDto>(_mapper.ConfigurationProvider);
    }
}