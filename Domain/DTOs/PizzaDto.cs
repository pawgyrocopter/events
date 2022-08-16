using Domain.Entities;

namespace Domain.DTOs;

public class PizzaDto
{
    public int Id { get; set; }
    public State State { get; set; }
    public string Name { get; set; }
    public string Ingredients { get; set; }
    public int Cost { get; set; }
    public int Weight { get; set; }
    public string? PhotoUrl { get; set; }
    public IEnumerable<TopingDto>? Topings { get; set; }
    
}