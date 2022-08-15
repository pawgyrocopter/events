using PizzaApp.DTOs;
using PizzaApp.Entities;

namespace PizzaApp.Interfaces;

public interface IUserService
{
    Task<IEnumerable<User>> GetUsers();

}