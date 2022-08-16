using Domain.DTOs;
using Microsoft.AspNetCore.Http;

namespace Domain.Interfaces.IServices;

public interface IPizzaService
{
    Task<IEnumerable<PizzaDto>> GetPizzas();
    Task<PizzaDto> GetPizza(string name);
    Task<PizzaDto> GetPizzaByName(string pizzaName);
    Task<PizzaDto> AddPizza(IFormFile file,PizzaDto pizzaDto);
    Task<PizzaDto> UpdatePizza(PizzaDto pizzaDto);
    Task<PizzaDto> UpdatePizzaOrderState(int pizzaId, int state);

    Task<IEnumerable<PizzaDto>> GetPizzasByOrderId(int orderId);


}