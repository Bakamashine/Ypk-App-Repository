namespace Application.Dtos.StatusProducts;

public class StatusProductListVm
{
    public IList<StatusProductLookupDto> StatusProducts { get; set; } = new List<StatusProductLookupDto>();
}