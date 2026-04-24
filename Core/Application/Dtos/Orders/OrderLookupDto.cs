using Application.Common.Mappings;
using Application.Dtos.Products;
using AutoMapper;
using Domain.Model;

namespace Application.Dtos.Orders;

public class OrderLookupDto : IMapWith<Order>
{
    public Guid Id { get; set; }
    public Guid ExecutorId { get; set; }
    public Guid CustomerId { get; set; }
    public DateTime Date { get; set; }
    public string? StatusName { get; set; }
    public string? CustomersComment { get; set; }
    public string? UserComment { get; set; }

    public ProductLookupDto ProductDto { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<Order, OrderLookupDto>()
            .ForMember(productVm => productVm.Id,
                opt => opt.MapFrom(product => product.Id))
            .ForMember(productVm => productVm.ExecutorId,
                opt => opt.MapFrom(product => product.ExecutorId))
            .ForMember(productVm => productVm.CustomerId,
                opt => opt.MapFrom(product => product.CustomerId))
            .ForMember(productVm => productVm.Date,
                opt => opt.MapFrom(product => product.Date))
            .ForMember(productVm => productVm.CustomersComment,
                opt => opt.MapFrom(product => product.CustomersComment))
            .ForMember(productVm => productVm.UserComment,
                opt => opt.MapFrom(product => product.UserComment))
            .ForMember(productVm => productVm.ProductDto,
                opt => opt.MapFrom(product => product.Product))
            .ForMember(productVm => productVm.StatusName,
                opt => opt.MapFrom(product => product.StatusOrder.StatusName));
    }
}