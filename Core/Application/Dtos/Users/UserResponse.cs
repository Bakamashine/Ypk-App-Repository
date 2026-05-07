using Domain.Model;
namespace Application.Dtos.Users;

public class UserResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    // public string HashPassword { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public string? UserInfo { get; set; }
    // public bool IsActive { get; set; }

    // public string? AvatarPath { get; set; }
    public string? AvatarUrl { get; set; }

    private static string? Url {get; set;}

    public UserResponse() {}
    public static void SetUrl(string url)
    { 
        Url = url;
        // return this;
    }

    public UserResponse(User user)
    {
        Id = user.Id;
        Name = user.Fullname;
        PhoneNumber = user.PhoneNumber;
        UserInfo = user.UserInfo;
        // AvatarUrl = Url + user.AvatarPath;
    }
}