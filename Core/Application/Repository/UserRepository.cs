using Application.Interfaces;
using Application.Interfaces.Repository;
using Domain.Model;
using Microsoft.EntityFrameworkCore;

namespace Application.Repository;

public class UserRepository : IUserRepository
{
    private readonly IApplicationDbContext _context;

    public UserRepository(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<User?> GetByPhoneNumber(string phone)
    {
        return await _context
            .Users
            .Where(user => user.PhoneNumber == phone)
            .AsNoTracking()
            .FirstAsync();
    }

    public async Task<User?> GetByRefreshToken(string token)
    {
        var record = await _context.UserToken
            .FirstOrDefaultAsync(r => r.Token == token && r.ExpiresOnUtc > DateTime.UtcNow);
        if (record == null) return null;
        var currentUser = await _context.Users.FindAsync(record.UserId);
        return currentUser;
    }
}