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

    public async Task<IQueryable<Pizza>> GetPizzas()
    {
        return _context.Pizzas
            .Include(p => p.Photo);
    }


    public async Task<Pizza> GetPizza(string name)
    {
        var pizza = await _context.Pizzas
            .Include(p => p.Photo)
            .FirstOrDefaultAsync(x => x.Name == name);
        return pizza;
    }

    public async Task<Pizza> GetPizzaByName(string pizzaName)
    {
        return await _context.Pizzas.FirstOrDefaultAsync(x => x.Name.Equals(pizzaName));
    }

   

    public async Task<Pizza> AddPizza(Pizza pizza,Photo photo)
    {
        _context.Pizzas.Add(pizza);
        _context.Photos.Add(photo);
        //Test sync threads
        return pizza;
    }

    public async Task<Pizza> UpdatePizza(Pizza pizza)
    {
        _context.Pizzas.Update(pizza);
        return pizza;
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
    public async Task<PizzaOrder> GetPizzaOrderById(int pizzaOrderId)
    {
        return  await _context.PizzaOrders.Include(x => x.Order).ThenInclude(d => d.Pizzas)
            .FirstOrDefaultAsync(x => x.Id == pizzaOrderId);
    }
    public async Task<IQueryable<PizzaOrder>> GetPizzasByOrderId(int orderId)
    {
        return _context.PizzaOrders.Where(x => x.OrderId == orderId);
    }
    
}