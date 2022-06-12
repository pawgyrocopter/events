using Microsoft.AspNetCore.Mvc;
using PizzaApp.DTOs;
using PizzaApp.Entities;

namespace PizzaApp.Interfaces;

public interface IPizzaRepository
{
    Task<IEnumerable<PizzaDto>> GetPizzas();
    Task<ActionResult<PizzaDto>> GetPizza(string name);
    Task<ActionResult<Pizza>> AddPizza(IFormFile file, [FromQuery] PizzaDto pizzaDto);//should be changed
    Task<ActionResult<PizzaDto>> UpdatePizza(PizzaDto pizzaDto);
    Task<PizzaDto> UpdatePizzaOrderState(int pizzaId, int state);
    Task<IEnumerable<PizzaDto>> GetPizzasByOrderId(int orderId);
}