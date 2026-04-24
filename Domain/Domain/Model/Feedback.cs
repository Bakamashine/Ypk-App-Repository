using Domain.Model.Base;

namespace Domain.Model;

public class Feedback : BaseModel
{
    public Guid UserId { get; set; }
    public string Comment { get; set; } = string.Empty;
    public int Raiting { get; set; }
    public string? ImagePath { get; set; }

    public User? User { get; set; }
}