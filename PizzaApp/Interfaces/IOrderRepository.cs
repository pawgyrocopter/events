using PizzaApp.DTOs;
using PizzaApp.Entities;

namespace PizzaApp.Interfaces;

public interface IOrderRepository
{
    Task<OrderDto> CreateOrder(OrderDto orderDto);
    Task<IEnumerable<OrderDto>> GetOrders();

    Task<IEnumerable<OrderDto>> GerUserOrders(string name);
    Task<OrderDto> GetOrderById(int orderId);
    Task<bool> UpdateOrderState(int orderId);
}