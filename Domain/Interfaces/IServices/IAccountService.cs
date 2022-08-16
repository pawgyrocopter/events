using Domain.DTOs;

namespace Domain.Interfaces.IServices;

public interface IAccountService
{
    Task<UserDto> Register(RegisterDto registerDto);
    Task<UserDto> Login(LoginDto loginDto);
}