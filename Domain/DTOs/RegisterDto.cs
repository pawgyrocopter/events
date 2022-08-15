using System.ComponentModel.DataAnnotations;

namespace Domain.DTOs;

public class RegisterDto
{
    [Required] public string Name { get; set; }

    [Required] public string Email { get; set; }


    [Required] public string Adress { get; set; }

    [Required] public string PhoneNumber { get; set; }

    [Required]
    [StringLength(8, MinimumLength = 4)]
    public string Password { get; set; }
}