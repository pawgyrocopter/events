using System.ComponentModel.DataAnnotations;

namespace Domain.DTOs;

public class RegisterDto
{
    public string UserName { get; set; }

    [Required] 
    public string Email { get; set; }
    [Required]
    public string Password { get; set; }
}