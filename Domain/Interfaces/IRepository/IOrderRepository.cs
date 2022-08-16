using Domain.DTOs;
using Domain.Entities;

namespace Domain.Interfaces.IRepository;

public interface IOrderRepository
{
    Task<OrderDto> CreateOrder(Order order);
    Task<IQueryable<Order>> GetOrders();

    Task<IQueryable<Order>> GerUserOrders(string name);
    Task<OrderDto> GetOrderById(int orderId);
    Task<Order> GetOrderByIdAsync(int orderId);
    Task<Order> GetOrderByPizzaId(int pizzaOrderId);
}