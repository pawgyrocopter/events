using AutoMapper;
using Domain.Interfaces.IRepository;

namespace Infrastructure.Data;

public class UnitOfWork : IUnitOfWork
{
    private readonly DataContext _context;

    public UnitOfWork(DataContext context,
        IUserRepository userRepository)
    {
        _context = context;
        UserRepository = userRepository;
    }

    public IUserRepository UserRepository { get; } 
    
    public async Task<bool> Complete()
    {
        return await _context.SaveChangesAsync() > 0;
    }

}