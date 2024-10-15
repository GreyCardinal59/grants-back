using admin_back.DataAccess;
using admin_back.Interfaces;
using admin_back.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace admin_back.Services;

public class UsersService
{
    private readonly UsersDbContext _usersDbContext;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IJwtProvider _jwtProvider;
    
    public UsersService(UsersDbContext dbContext, IPasswordHasher passwordHasher, IJwtProvider jwtProvider)
    {
        _usersDbContext = dbContext;
        _passwordHasher = passwordHasher;
        _jwtProvider = jwtProvider;
    }
    
    public async Task Register(string userName, string password)
    {
        var hashedPassword = _passwordHasher.Generate(password);

        var user = User.Create(Guid.NewGuid(), userName, hashedPassword);
        
        await _usersDbContext.AddAsync(user);
        await _usersDbContext.SaveChangesAsync();
    }

    public async Task<string> Login(string login, string password)
    {
        var user = await _usersDbContext.Users.SingleOrDefaultAsync(u => u.Login == login);
        
        if (user == null)
        {
            throw new Exception("User not found");
        }

        var result = _passwordHasher.Verify(password, user.PasswordHash);

        if (result == false)
        {
            throw new Exception("Failed to login");
        }

        var token = _jwtProvider.GenerateToken(user);
        return token;
    }
}