using PizzaApp.DTOs;
using PizzaApp.Entities;

namespace PizzaApp.Interfaces;

public interface IOrderRepository
{
    Task<Order> CreateOrder(OrderDto orderDto);
}