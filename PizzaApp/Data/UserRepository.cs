using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PizzaApp.Entities;
using PizzaApp.Interfaces;

namespace PizzaApp.Data;

public class UserRepository : IUserRepository
{
    private readonly DataContext _context;
    private readonly IMapper _mapper;

    public UserRepository(DataContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
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