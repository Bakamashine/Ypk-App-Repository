using Application.Dtos.Auth;
using Domain.Model;

namespace Application.Interfaces;

public interface IJwtTokenService
{
    public Task<TokenDto?> GenerateToken(User user);
    public Task<string> GenerateRefreshToken(User user, CancellationToken cancellationToken = default);
    public Task<string> GenerateJwtToken(User user, CancellationToken cancellationToken = default);
    public Task<bool> ValidateRefreshTokenAsync(string refreshToken);
}