using PizzaApp.Entities;

namespace PizzaApp.Interfaces;

public interface IUserRepository
{
    Task<IEnumerable<User>> GetUsers();
}