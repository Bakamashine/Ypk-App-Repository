namespace Application.Interfaces;

public interface IUrlHelper
{
    string GetFullUrl(string? relativePath);
}