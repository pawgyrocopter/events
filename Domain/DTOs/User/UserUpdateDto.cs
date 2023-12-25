namespace Domain.DTOs.User;

public record UserUpdateDto(string Name, string Email, List<PhotoDto> Photos)
{
    
}