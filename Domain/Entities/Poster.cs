using Domain.DTOs;

namespace Domain.Entities;

public class Poster
{
    public Guid Id { get; set; }

    public string Name { get; set; }

    public string Description { get; set; }
    
    public DateTime From { get; set; }
    public DateTime To { get; set; }
    
    public Guid PhotoId { get; set;}

    public string Base64Photo { get; set; }

    public List<Event> Events { get; set; }

    public Poster()
    {
    }

    public Poster(PosterCreateDto dto)
    {
        Id = Guid.NewGuid();
        Name = dto.Name;
        Description = dto.Name;
        Base64Photo = dto.Base64Photo;
        To = dto.To;
        From = dto.From;
    }
}