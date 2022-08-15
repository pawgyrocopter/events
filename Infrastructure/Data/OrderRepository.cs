using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain.DTOs;
using Domain.Entities;
using Domain.Interfaces.IRepository;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;

public class OrderRepository : IOrderRepository
{
    private readonly DataContext _context;
    private readonly IMapper _mapper;

    public OrderRepository(DataContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<OrderDto> CreateOrder(Order order)
    {
        // var order = new Order();
        // var users = await _context.Users.ToListAsync();
        // order.User = await _context.Users.FirstOrDefaultAsync(x => x.UserName.Equals(orderDto.Name));
        // var pizzas = new List<PizzaOrder>();
        // foreach (var pizza in orderDto.Pizzas)
        // {
        //     var pOrder = new PizzaOrder();
        //     pOrder.Pizza = await _context.Pizzas.FirstOrDefaultAsync(x => x.Name == pizza.Name);
        //     pOrder.Topings = pizza.Topings
        //         .Select(x => new TopingOrder()
        //         {
        //             Toping = _context.Topings.FirstOrDefaultAsync(t => t.Id == x.Id).Result,
        //             Counter = x.Counter
        //         }).ToList();
        //     pOrder.Pizza.Cost = pizza.Cost;
        //     pOrder.State = State.Pending;
        //     pizzas.Add(pOrder);
        // }
        //
        // order.Pizzas = pizzas;
        await _context.Orders.AddAsync(order);
        await _context.SaveChangesAsync();
        return _mapper.Map<OrderDto>(order);
    }

    public async Task<IQueryable<Order>> GetOrders()
    {
        return _context.Orders
            .Include(x => x.User)
            .Include(x => x.Pizzas)
            .ThenInclude(x => x.Pizza);
    }

    public async Task<IQueryable<Order>> GerUserOrders(string name)
    {
        return  _context.Orders
            .Include(x => x.User)
            .Include(x => x.Pizzas)
            .ThenInclude(x => x.Pizza)
            .Where(x => x.User.UserName.Equals(name));
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
    
    public async Task<Order> GetOrderByIdAsync(int orderId)
    {
        return await _context.Orders
            .Include(x => x.User)
            .Include(x => x.Pizzas)
            .ThenInclude(x => x.Pizza)
            .FirstOrDefaultAsync(x => x.Id == orderId);
    }

    public async Task<Order> GetOrderByPizzaId(int pizzaOrderId)
    {
       return await _context.Orders
           .Include(p => p.Pizzas)
           .FirstOrDefaultAsync(x => x.Id == pizzaOrderId);
    }
}