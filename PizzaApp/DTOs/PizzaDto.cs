namespace PizzaApp.DTOs;

public class PizzaDto
{
    public string Name { get; set; }
    public string Ingredients { get; set; }
    public int Cost { get; set; }
    public int Weight { get; set; }
    public string? PhotoUrl { get; set; }
    public IEnumerable<TopingDto>? Topings { get; set; }
    
}