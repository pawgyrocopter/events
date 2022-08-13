using Microsoft.AspNetCore.Mvc;
using PizzaApp.DTOs;

namespace PizzaApp.Interfaces;

public interface IPizzaService
{
    Task<IEnumerable<PizzaDto>> GetPizzas();
    Task<ActionResult<PizzaDto>> GetPizza(string name);
    Task<PizzaDto> GetPizzaByName(string pizzaName);
    Task<ActionResult<PizzaDto>> AddPizza(IFormFile file, [FromQuery] PizzaDto pizzaDto);
    Task<ActionResult<PizzaDto>> UpdatePizza(PizzaDto pizzaDto);
    Task<PizzaDto> UpdatePizzaOrderState(int pizzaId, int state);

    Task<IEnumerable<PizzaDto>> GetPizzasByOrderId(int orderId);


}