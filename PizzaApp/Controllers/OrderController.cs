using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Authorization;
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

[Authorize]
public class OrderController : BaseApiController
{
    private readonly IHubContext<OrderHub, IOrderHub> _hub;
    private readonly IOrderService _orderService;

    public OrderController(IHubContext<OrderHub, IOrderHub> hub, IOrderService orderService)
    {
        _hub = hub;
        _orderService = orderService;
    }

    [HttpPost("create-order")]
    public async Task<OrderDto> CreateOrder(OrderDto orderDto)
    {
        var order = await _orderService.CreateOrderAsync(orderDto);
        await _hub.Clients.All.SendMessage();
        return order;
    }

    [HttpGet()]
    public async Task<IEnumerable<OrderDto>> GetOrders()
    {
        return await _orderService.GetOrdersAsync();
    }

    [HttpGet("get-user-orders/{name}")]
    public async Task<IEnumerable<OrderDto>> GerUserOrders(string name)
    {
        return await _orderService.GetUserOrdersAsync(name);
    }

    [HttpGet("{orderId}")]
    public async Task<OrderDto> GetOrderById(int orderId)
    {
        return await _orderService.GerOrderById(orderId);
    }

    [Authorize(Roles = "PizzaMaker")]
    [HttpPut("{orderId}")]
    public async Task<bool> UpdateOrderState(int orderId)
    {
        return await _orderService.UpdateOrderState(orderId);
    }
}