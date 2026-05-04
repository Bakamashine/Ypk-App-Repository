using Domain.Model.Base;

namespace Domain.Model;

public class RefreshToken : BaseModel
{
    public string Token { get; set; }
    public Guid UserId { get; set; }
    public DateTime ExpiresOnUtc { get; set; }
    public User User { get; set; }
}