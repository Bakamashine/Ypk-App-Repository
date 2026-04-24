using Domain.Model.Base;

namespace Domain.Model;

public class Role : BaseModel
{
    public string RoleName { get; set; } = string.Empty;

    public ICollection<User>? Users { get; set; }
}