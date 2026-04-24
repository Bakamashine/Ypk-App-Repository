namespace Application.Dtos.Products;

public class ProductListVm
{
    public IList<ProductLookupDto> Products { get; set; } = new List<ProductLookupDto>();
}