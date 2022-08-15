using Domain.Entities;

namespace Domain.Interfaces.IServices;

public interface ITokenService
{
    Task<string> CreateToken(User user);
}