using Domain.Model.Base;

namespace Domain.Model;

public class StatusProduct : BaseModel
{
    public string StatusName { get; set; } = string.Empty;


    public ICollection<Product>? Products { get; set; }
}