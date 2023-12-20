using AutoMapper;
using Domain.Entities;
using Domain.Interfaces.IRepository;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;

public class UserRepository : IUserRepository
{
    private readonly DataContext _context;

    public UserRepository(DataContext context)
    {
        _context = context;
    }
    
    public async Task<IQueryable<User>> GetUsers()
    {
        return _context.Users;
    }

    public async Task<User> GetUserByName(string name)
    {
        return await _context.Users.FirstOrDefaultAsync(x => x.UserName == name);
    }
}