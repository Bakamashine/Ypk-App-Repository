using System.ComponentModel.DataAnnotations;

namespace CourseWebApi.Models.Auth;

public class LoginViaTokenRequest
{
    [Required] public string refreshToken { set; get; } = string.Empty;
}