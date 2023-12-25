namespace Domain.DTOs;

public record PosterCreateDto
{
    public string Name { get; set; }
    
    public string Base64Photo { get; set; }
    
    public string Description { get; set; }
    
    public DateTime DateTime { get; set; }
}