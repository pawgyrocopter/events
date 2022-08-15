using Domain.Entities;

namespace Domain.Interfaces.IServices;

public interface IUserService
{
    Task<IEnumerable<User>> GetUsers();

}