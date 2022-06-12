using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PizzaApp.Data;
using PizzaApp.DTOs;
using PizzaApp.Entities;
using PizzaApp.Interfaces;
using PizzaApp.SignalR;

namespace PizzaApp.Controllers;

public class OrderController : BaseApiController
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly DataContext _context;
    private readonly IHubContext<OrderHub> _hub;

    public OrderController(IUnitOfWork unitOfWork, IMapper mapper, DataContext context)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _context = context;
    }
    [HttpPost("create-order")]
    public async Task<OrderDto> CreateOrder(OrderDto orderDto)
    {
       var order =  await _unitOfWork.OrderRepository.CreateOrder(orderDto);
        await _unitOfWork.Complete();
        return order;
    }

    [HttpGet()]
    public async Task<IEnumerable<OrderDto>> GetOrders()
    {
        return await _unitOfWork.OrderRepository.GetOrders();
        // return await _context.Orders
        //     .Include(x => x.User)
        //     .Include(x => x.Pizzas)
        //     .ThenInclude(x => x.Pizza)
        var pizzas = await _context.PizzaOrders
            .Include(t => t.Topings)
            .ProjectTo<PizzaDto>(_mapper.ConfigurationProvider)
            .ToListAsync();
        return null;
    }

    [HttpGet("get-user-orders/{name}")]
    public async Task<IEnumerable<OrderDto>> GerUserOrders(string name)
    {
        return await _unitOfWork.OrderRepository.GerUserOrders(name);
    }

    [HttpGet("{orderId}")]
    public async Task<OrderDto> GetOrderById(int orderId)
    {
        return await _unitOfWork.OrderRepository.GetOrderById(orderId);
    }

    [HttpPut("{orderId}")]
    public async Task<bool> UpdateOrderState(int orderId)
    {
        return await _unitOfWork.OrderRepository.UpdateOrderState(orderId);
    }

}