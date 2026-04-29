using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Application.Dtos.Auth;
using Application.Interfaces;
using Application.Interfaces.Repository;
using Domain.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace ProductsWebApi.Services;

public class JwtTokenService : IJwtTokenService
{
    private readonly IApplicationDbContext context;
    private readonly TimeSpan expiryDuration = new(0, 5, 0);
    private readonly string secretKey;


    public JwtTokenService(IApplicationDbContext context, IConfiguration configuration)
    {
        this.context = context;
        secretKey = configuration["SECRET_KEY"];
    }

    public async Task<TokenDto?> GenerateToken(User user)
    {
        var accessToken = await GenerateJwtToken(user);
        return new TokenDto { AccessToken = accessToken };
    }


    public async Task<string> GenerateJwtToken(User user, CancellationToken cancellationToken = default)
    {
        var role = await context.Roles.FindAsync(new object[] { user.RoleId }, cancellationToken);
        var roleNameClaim = role?.RoleName ?? string.Empty;

        var tokenHandler = new JwtSecurityTokenHandler();
        // var key = Encoding.ASCII.GetBytes(SECRET_KEY);
        var tokenDescription = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.Name, user.Fullname),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Role, roleNameClaim),
                new Claim(ClaimTypes.MobilePhone, user.PhoneNumber)
            }),
            Expires = DateTime.UtcNow.Add(expiryDuration),
            SigningCredentials = new SigningCredentials(GetSymmetricKey(),
                SecurityAlgorithms.HmacSha256Signature),
            Audience = "ProductWebApi"
        };

        var token = tokenHandler.CreateToken(tokenDescription);
        return tokenHandler.WriteToken(token);
    }

    public async Task<string> GenerateRefreshToken(User user, CancellationToken cancellationToken = default)
    {
        var signingCred = new SigningCredentials(GetSymmetricKey(), SecurityAlgorithms.HmacSha256Signature);
        var expirationDate = DateTime.UtcNow.AddHours(12);
        var jwtSecurityToken = new JwtSecurityToken(
            expires: expirationDate,
            signingCredentials: signingCred,
            notBefore: DateTime.UtcNow,
            claims: null
        );
        var refreshToken = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
        var refreshTokenEntity = new RefreshToken
        {
            Id = Guid.NewGuid(),
            UserId = user.Id,
            Token = refreshToken,
            ExpiresOnUtc = DateTime.UtcNow.AddDays(7)
        };
        await context.UserToken.AddAsync(refreshTokenEntity);
        await context.SaveChangesAsync(cancellationToken);
        return refreshToken;
    }

    public async Task<bool> ValidateRefreshTokenAsync(string refreshToken)
    {
        var tokenEntity = await context.UserToken
            .FirstOrDefaultAsync(t => t.Token == refreshToken && t.ExpiresOnUtc > DateTime.UtcNow);
        return tokenEntity != null;
    }

    private SymmetricSecurityKey GetSymmetricKey()
    {
        return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(secretKey));
    }
}