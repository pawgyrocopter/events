using AutoMapper;
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

    public async Task<Order> CreateOrder(OrderDto orderDto)
    {
        // var order = new Order()
        // {
        //     UserId = _context.Users.FirstOrDefaultAsync(x => x.UserName == orderDto.Name).Id
        // };
        //
        // _context.Orders.Add(order);
        // await _context.SaveChangesAsync();
        return new Order();
    }
}