using Application.Dtos.Products;
using Application.Queries.Base;

namespace Application.Queries.Products.GetProductPagList;

public class GetAllProductPagQuery : PagedQuery<ProductLookupDto>
{
    public string SearchText { get; set; } = string.Empty;
}
