using Microsoft.AspNetCore.Mvc;
using PizzaApp.DTOs;
using PizzaApp.Entities;

namespace PizzaApp.Interfaces;

public interface IPizzaRepository
{
    Task<IQueryable<Pizza>> GetPizzas();
    Task<Pizza> GetPizza(string name);
    Task<Pizza> AddPizza(Pizza pizza, Photo photo);//should be changed
    Task<Pizza> UpdatePizza(Pizza pizza);
    Task<PizzaDto> UpdatePizzaOrderState(int pizzaId, int state);
    Task<IQueryable<PizzaOrder>> GetPizzasByOrderId(int orderId);

    Task<Pizza> GetPizzaByName(string pizzaName);

    Task<PizzaOrder> GetPizzaOrderById(int pizzaOrderId);
}