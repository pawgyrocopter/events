namespace Domain.DTOs.Events;

public class EventCreateDto
{
    public string Name { get; set; }
    
    public string Address { get; set; }
    
    public string ShortDescription { get; set; }
    
    public string Description { get; set; }
    
    public DateTime From { get; set; }
    
    public DateTime To { get; set; }
    
    public Guid? PosterId { get; set; }
}