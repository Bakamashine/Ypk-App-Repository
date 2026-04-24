namespace Application.Dtos.Orders;

public class OrderListVm
{
    public IList<OrderLookupDto> Orders { get; set; } = new List<OrderLookupDto>();
}