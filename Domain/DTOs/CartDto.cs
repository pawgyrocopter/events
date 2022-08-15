namespace Domain.DTOs;

public class CartDto
{
    public IEnumerable<PizzaDto> Pizzas { get; set; }
}