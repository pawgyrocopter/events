namespace PizzaApp.DTOs;

public class OrderDto
{
    public string Name { get; set; }
    public int OrderId { get; set; }
    public IEnumerable<PizzaDto> Pizzas { get; set; }
}