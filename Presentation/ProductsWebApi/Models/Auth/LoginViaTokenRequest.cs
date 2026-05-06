using System.ComponentModel.DataAnnotations;

namespace CourseWebApi.Models.Auth;

public class LoginViaRefreshTokenRequest
{
    public string? refreshToken { set; get; } = string.Empty;
}