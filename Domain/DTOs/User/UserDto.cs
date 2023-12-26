namespace Domain.DTOs;

public record UserDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Token { get; set; }
    public string Email { get; set; }
}