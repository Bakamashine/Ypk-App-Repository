using Application.Dtos.Auth;
using Domain.Model;

namespace Application.Interfaces;

public interface IJwtTokenServise
{
    public Task<TokensDto> GenerateToken(User user);
}