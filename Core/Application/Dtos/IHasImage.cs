namespace Application.Dtos;

public interface IHasImage
{
    public string? ImagePath { get; set; }
    public string? ImageUrl { get; set; }
}