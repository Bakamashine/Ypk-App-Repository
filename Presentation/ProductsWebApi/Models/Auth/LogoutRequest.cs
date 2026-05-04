using System.ComponentModel.DataAnnotations;

namespace CourseWebApi.Models.Auth;

public class LogoutRequest
{
    [Required] public string refreshToken { get; set; } = string.Empty;
}
