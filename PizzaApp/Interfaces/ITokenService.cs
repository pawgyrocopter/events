using PizzaApp.Entities;

namespace PizzaApp.Interfaces;

public interface ITokenService
{
    Task<string> CreateToken(User user);
}