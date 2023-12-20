using AutoMapper;
using Domain.Entities;
using Domain.Interfaces.IRepository;
using Domain.Interfaces.IServices;

namespace Infrastructure.Services;

public class UserService : IUserService
{
    private readonly IUnitOfWork _unitOfWork;

    public UserService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<User>> GetUsers()
    {
        return await _unitOfWork.UserRepository.GetUsers();
    }
}