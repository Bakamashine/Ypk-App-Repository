namespace Application.Dtos.StatusOrders;

public class StatusOrderListVm
{
    public IList<StatusOrderLookupDto> StatusOrders { get; set; } = new List<StatusOrderLookupDto>();
}