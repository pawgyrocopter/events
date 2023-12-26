using Domain.DTOs;
using Domain.Entities;
using Domain.Interfaces.IServices;
using Infrastructure.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services;

public class AccountService : IAccountService
{
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;
    private readonly ITokenService _tokenService;
    private readonly DataContext _context;

    public AccountService(UserManager<User> userManager, SignInManager<User> signInManager,
        ITokenService tokenService, DataContext context)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _tokenService = tokenService;
        _context = context;
    }

    public async Task<UserDto> Register(RegisterDto registerDto)
    {
        if (await UserExists(registerDto.Email))
            throw new UnauthorizedAccessException("Email is already in use");
        
        if (await UserNameExists(registerDto.UserName ?? registerDto.Email))
            throw new UnauthorizedAccessException("UserName is already in use");

        var user = new User()
        {
            Email = registerDto.Email,
            UserName = registerDto.UserName ?? registerDto.Email,
        };

        user.Email = registerDto.Email.ToLower();

        var result = await _userManager.CreateAsync(user, registerDto.Password);
        if (!result.Succeeded)
        {
            Console.WriteLine(result.Errors.ToString());
            throw new ApplicationException(result.Errors.ToString());
        }

        var roleResult = await _userManager.AddToRoleAsync(user, "Customer");
        if (!roleResult.Succeeded)
        {
            Console.WriteLine(result.Errors.ToString());
            throw new ApplicationException(result.Errors.ToString());
        }

        return new UserDto()
        {
            Id = user.Id,
            Email = registerDto.Email,
            Name = registerDto.UserName,
            Token = await _tokenService.CreateToken(user)
        };
    }

    public async Task<UserDto> Login(LoginDto loginDto)
    {
        var user = await _userManager.Users
            .FirstOrDefaultAsync(x => x.Email.Equals(loginDto.Email.ToLower()));

        if (user == null)
            throw new UnauthorizedAccessException("Email doesn't exist");

        var result = await _signInManager
            .CheckPasswordSignInAsync(user, loginDto.Password, false);

        if (!result.Succeeded)
            throw new UnauthorizedAccessException();

        return new UserDto()
        {
            Id = user.Id,
            Name = user.UserName,
            Token = await _tokenService.CreateToken(user),
            Email = user.Email
        };
    }

    private async Task<bool> UserExists(string email)
    {
        return await _userManager.Users.AnyAsync(x => x.Email.Equals(email.ToLower()));
    }
    
    private async Task<bool> UserNameExists(string username)
    {
        return await _userManager.Users.AnyAsync(x => x.UserName.Equals(username.ToLower()));
    }
}