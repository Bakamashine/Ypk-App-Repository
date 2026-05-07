using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Application.Dtos.Auth;
using Application.Interfaces;
using Application.Interfaces.Repository;
using Domain.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace ProductsWebApi.Services;

public class JwtTokenService : IJwtTokenService
{
    private readonly IApplicationDbContext context;
    private readonly TimeSpan expiryDuration = new(0, 5, 0);
    private readonly string secretKey;
    private readonly string _refreshTokenKey = "refreshToken";
    private readonly IHttpContextAccessor _httpContextAccessor;

    private HttpContext GetHttpContext()
    {
        if (_httpContextAccessor == null) throw new NullReferenceException(nameof(_httpContextAccessor));
        var httpContext = _httpContextAccessor.HttpContext ??
                          throw new InvalidOperationException("Not active Http context");
        return httpContext;
    }

    public JwtTokenService(IApplicationDbContext context, IConfiguration configuration,
        IHttpContextAccessor httpContextAccessor)
    {
        this.context = context;
        secretKey = configuration["SECRET_KEY"];
        _httpContextAccessor = httpContextAccessor;
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
                new Claim(ClaimTypes.MobilePhone, user.PhoneNumber),
                new Claim("avatarPath", user.AvatarPath ?? ""),
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
        var refreshToken = Convert.ToBase64String(RandomNumberGenerator.GetBytes(32));

        var expires = DateTime.UtcNow.AddDays(7);
        var refreshTokenEntity = new RefreshToken
        {
            Id = Guid.NewGuid(),
            UserId = user.Id,
            Token = refreshToken,
            ExpiresOnUtc = expires
        };

        var cookieOptions = new CookieOptions()
        {
            Expires = expires,
            HttpOnly = true,
            Secure = true,
            IsEssential = true,
            SameSite = SameSiteMode.None
        };

        GetHttpContext().Response.Cookies.Append(_refreshTokenKey, refreshToken, cookieOptions);
        await context.UserToken.AddAsync(refreshTokenEntity, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
        return refreshToken;
    }

    public async Task<bool> ValidateRefreshTokenAsync(string refreshToken)
    {
        var tokenEntity = await context.UserToken
            .FirstOrDefaultAsync(t => t.Token == refreshToken && t.ExpiresOnUtc > DateTime.UtcNow);
        return tokenEntity != null;
    }

    public async Task InvalidateRefreshTokenAsync(string token, CancellationToken cancellationToken = default)
    {
        var cookieOptions = new CookieOptions()
        {
            HttpOnly = true,
            Secure = true,
            SameSite = SameSiteMode.None
        };
        var httpContext = GetHttpContext();
        // httpContext.Response.Cookies.Delete(_refreshTokenKey, cookieOptions);
        var recordWithRefreshToken = await context.UserToken
            .FirstOrDefaultAsync(e => e.Token == token, cancellationToken);
        if (recordWithRefreshToken != null)
        {
            context.UserToken.Remove(recordWithRefreshToken);
            await context.SaveChangesAsync(cancellationToken);
        }
    }

    private SymmetricSecurityKey GetSymmetricKey()
    {
        return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(secretKey));
    }
}