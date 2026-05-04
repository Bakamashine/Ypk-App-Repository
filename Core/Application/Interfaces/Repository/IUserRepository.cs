using Domain.Model;

namespace Application.Interfaces.Repository;

public interface IUserRepository
{
    public Task<User?> GetByPhoneNumber(string phone);
    public Task<User?> GetByRefreshToken(string token);
}