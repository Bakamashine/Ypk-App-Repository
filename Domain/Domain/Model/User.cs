using Domain.Model.Base;

namespace Domain.Model;

public class User : BaseModel
{
    public string Fullname { get; set; } = string.Empty;
    public string HashPassword { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public Guid RoleId { get; set; }
    public Guid? YpkId { get; set; }
    public string? UserInfo { get; set; }
    public bool IsActive { get; set; }

    public Role? Role { get; set; }
    public Ypk? Ypk { get; set; }

    public ICollection<Feedback>? Feedbacks { get; set; }
    public ICollection<Product>? Products { get; set; }
    public ICollection<Order>? Orders { get; set; }
}