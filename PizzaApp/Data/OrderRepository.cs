using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using PizzaApp.DTOs;
using PizzaApp.Entities;
using PizzaApp.Interfaces;

namespace PizzaApp.Data;

public class OrderRepository : IOrderRepository
{
    private readonly DataContext _context;
    private readonly IMapper _mapper;

    public OrderRepository(DataContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<OrderDto> CreateOrder(OrderDto orderDto)
    {
        var order = new Order();
        var users = await _context.Users.ToListAsync();
        order.User = await _context.Users.FirstOrDefaultAsync(x => x.UserName.Equals(orderDto.Name));
        var pizzas = new List<PizzaOrder>();
        foreach (var pizza in orderDto.Pizzas)
        {
            var pOrder = new PizzaOrder();
            pOrder.Pizza = await _context.Pizzas.FirstOrDefaultAsync(x => x.Name == pizza.Name);
            pOrder.Topings = pizza.Topings
                .Select(x => new TopingOrder()
                {
                    Toping = _context.Topings.FirstOrDefaultAsync(t => t.Id == x.Id).Result,
                    Counter = x.Counter
                }).ToList();
            pOrder.Pizza.Cost = pizza.Cost;
            pizzas.Add(pOrder);
        }

        order.Pizzas = pizzas;
        await _context.Orders.AddAsync(order);
        return _mapper.Map<OrderDto>(order);
    }

    public async Task<IEnumerable<OrderDto>> GetOrders()
    {
        return await _context.Orders
            .Include(x => x.User)
            .Include(x => x.Pizzas)
            .ThenInclude(x => x.Pizza)
            .ProjectTo<OrderDto>(_mapper.ConfigurationProvider).ToListAsync();
    }

    public async Task<IEnumerable<OrderDto>> GerUserOrders(string name)
    {
        return await _context.Orders
            .Include(x => x.User)
            .Include(x => x.Pizzas)
            .ThenInclude(x => x.Pizza)
            .Where(x => x.User.UserName.Equals(name))
            .ProjectTo<OrderDto>(_mapper.ConfigurationProvider).ToListAsync();
    }

    public async Task<OrderDto> GetOrderById(int orderId)
    {
        var order = await _context.Orders
            .Include(x => x.User)
            .Include(x => x.Pizzas)
            .ThenInclude(x => x.Pizza)
            .ProjectTo<OrderDto>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(x => x.OrderId == orderId);
        return order;
    }
}