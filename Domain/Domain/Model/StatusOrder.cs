using Domain.Model.Base;

namespace Domain.Model;

public class StatusOrder : BaseModel
{
    public string StatusName { get; set; } = string.Empty;

    public ICollection<Order>? Orders { get; set; }
}