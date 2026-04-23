using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Application.Dtos.Auth;
using Application.Interfaces;
using Domain.Model;
using Microsoft.IdentityModel.Tokens;

namespace ProductsWebApi.Services;

public class JwtTokenService : IJwtTokenServise
{
    private readonly IProductsDbContext context;
    private readonly TimeSpan ExpiryDuration = new(30, 0, 0, 0);
    private readonly string SECRET_KEY;

    public JwtTokenService(IProductsDbContext context, IConfiguration configuration)
    {
        this.context = context;
        SECRET_KEY = configuration["SECRET_KEY"];
    }

    public async Task<TokensDto> GenerateToken(User user)
    {
        var accessToken = await GenerateJwtToken(user);
        return new TokensDto { AccessToken = accessToken };
    }

    private async Task<string> GenerateJwtToken(User user, CancellationToken cancellationToken = default)
    {
        var role = await context.Roles.FindAsync(new object[] { user.RoleId }, cancellationToken);
        var roleNameClaim = role?.RoleName ?? string.Empty;

        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(SECRET_KEY);
        var tokenDescription = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.Name, user.Fullname),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Role, roleNameClaim),
                new Claim(ClaimTypes.MobilePhone, user.PhoneNumber)
            }),
            Expires = DateTime.UtcNow.Add(ExpiryDuration),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha256Signature),
            Audience = "ProductWebApi"
        };

        var token = tokenHandler.CreateToken(tokenDescription);
        return tokenHandler.WriteToken(token);
    }
}