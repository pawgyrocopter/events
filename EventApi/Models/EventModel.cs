using EventDatabase.Entities;

namespace EventApi.Models;

public class EventModel
{
    public Guid Id { get; set; }
    
    public string Name { get; set; }


    public EventModel()
    {
    }

    public EventModel(EventEntity entity)
    {
        Id = entity.Id;
        Name = entity.Name;
    }
}