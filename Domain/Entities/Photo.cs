using System.Runtime.InteropServices;
using Domain.DTOs;

namespace Domain.Entities;

public class Photo
{
    public Guid Id { get; set; }
    
    public string Base64 { get; set; }
    
    public Photo(){}

    public Photo(PhotoDto photoDto)
    {
        Id = Guid.NewGuid();
        Base64 = photoDto.Base64;
    }
}