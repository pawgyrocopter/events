using PizzaApp.DTOs;
using PizzaApp.Entities;

namespace PizzaApp.Interfaces;

public interface IOrderRepository
{
    Task<OrderDto> CreateOrder(Order order);
    Task<IQueryable<Order>> GetOrders();

    Task<IQueryable<Order>> GerUserOrders(string name);
    Task<OrderDto> GetOrderById(int orderId);
    Task<Order> GetOrderByIdAsync(int orderId);
    Task<Order> GetOrderByPizzaId(int pizzaOrderId);
}