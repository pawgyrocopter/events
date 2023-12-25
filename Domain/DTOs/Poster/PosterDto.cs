namespace Domain.DTOs;

public record PosterDto
{
    public Guid Id { get; set; }
    
    public string Name { get; set; }
    
    public string Base64Photo { get; set; }
    
    public string Description { get; set; }
}