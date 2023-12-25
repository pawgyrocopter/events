using Domain.DTOs;

namespace Domain.Entities;

public class Poster
{
    public Guid Id { get; set; }

    public string Name { get; set; }

    public string Description { get; set; }
    
    public Guid PhotoId { get; set;}

    public Photo Photo { get; set; }

    public List<Event> Events { get; set; }

    public Poster()
    {
    }

    public Poster(PosterCreateDto dto)
    {
        Id = Guid.NewGuid();
        Name = dto.Name;
        Description = dto.Name;
        Photo = new Photo() {Id = Guid.NewGuid(), Base64 = dto.Base64Photo};
    }
}