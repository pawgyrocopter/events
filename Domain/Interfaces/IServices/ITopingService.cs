using Domain.DTOs;

namespace Domain.Interfaces.IServices;

public interface ITopingService
{
    Task<IEnumerable<TopingDto>> GetTopings();
    Task<TopingDto> CreateToping(string name);
    Task<TopingDto> GetTopingById(int topingId);
}