using Application.Dtos.Products;
using Application.Queries.Base;

namespace Application.Queries.Products.GetYpkPagListByYpk;

public class GetYpkPagListByYpkQuery : PagedQuery<ProductLookupDto>
{
    public Guid YpkId { get; set; }
}
