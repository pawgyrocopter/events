using PizzaApp.Entities;

namespace PizzaApp.Interfaces;

public interface IUserRepository
{
    Task<IQueryable<User>> GetUsers();
    Task<User> GetUserByName(string name);
}