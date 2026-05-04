using Application.Dtos;

namespace Application.Extensions;

public static class DtoExtensions
{
    public static void SetImageUrls<T>(this IEnumerable<T> items, string baseUrl) where T: IHasImage
    {
        foreach (var item in items)
            if (!string.IsNullOrEmpty(item.ImagePath))
                item.ImageUrl = $"{baseUrl}{item.ImagePath}";
                
    }
}