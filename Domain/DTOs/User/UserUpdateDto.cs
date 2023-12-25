namespace Domain.DTOs.User;

public record UserUpdateDto
{
    public string FirstName { get; set; }
    
    public string SecondName { get; set; }
    
    public int? Age { get; set; }
    
    public string ShortDescription { get; set; }
    
    public string Interests { get; set; }
    
    public string TelegramLink { get; set; }
    
    public string VKLink { get; set; }
    
    public string Base64Photo { get; set; }
    public string UserName { get; set; }
    public string Email { get; set; }
    public List<PhotoDto> Photos { get; set; }
}