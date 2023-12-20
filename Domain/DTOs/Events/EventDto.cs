using Domain.Entities;

namespace Domain.DTOs.Events;

public record EventDto
{
    public string Name { get; set; }
    
    public string Address { get; set; }
    
    public string ShortDescription { get; set; }
    
    public string Description { get; set; }
    
    public DateTime? Date { get; set; }
    
    public Guid CreatorId { get; set; }
    
    public EventDto(){}
    
    public EventDto(Event eventModel)
    {
        ShortDescription = eventModel.ShortDescription;
        Description = eventModel.Description;
        Address = eventModel.Address;
        Name = eventModel.Name;
        Date = eventModel.Date;
        CreatorId = eventModel.CreatorId;
    }
}