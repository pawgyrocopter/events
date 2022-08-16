using Domain.Entities;

namespace Domain.Interfaces.IRepository;

public interface ITopingRepository
{
    Task<IQueryable<Toping>> GetTopings();
    Task<Toping> CreateToping(string name);

    Task<Toping> GetTopingById(int topingId);
}