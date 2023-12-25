namespace Domain.DTOs.User;

public record UserCreatorDto
{
    public Guid Id { get; set; }
    
    public string Name { get; set; }
    
    public string Base64Photo { get; set; }
    
    public string Email { get; set; }
}