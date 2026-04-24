using Application.Common.Mappings;
using AutoMapper;
using Domain.Model;

namespace Application.Dtos.Products;

public class ProductLookupDto : IMapWith<Product>
{
    public Guid Id { get; set; }
    public Guid YpkId { get; set; }
    public string ProductName { get; set; } = string.Empty;
    public decimal ProductCost { get; set; }
    public string ProductInfo { get; set; } = string.Empty;
    public bool IsProduct { get; set; }

    public string? PhotoPath { get; set; }
    public string? PhotoUrl { get; set; }

    public string Adress { get; set; } = string.Empty;

    public void Mapping(Profile profile)
    {
        profile.CreateMap<Product, ProductLookupDto>()
            .ForMember(productVm => productVm.Id,
                opt => opt.MapFrom(product => product.Id))
            .ForMember(productVm => productVm.YpkId,
                opt => opt.MapFrom(product => product.YpkId))
            .ForMember(productVm => productVm.ProductName,
                opt => opt.MapFrom(product => product.ProductName))
            .ForMember(productVm => productVm.ProductCost,
                opt => opt.MapFrom(product => product.ProductCost))
            .ForMember(productVm => productVm.ProductInfo,
                opt => opt.MapFrom(product => product.ProductInfo))
            .ForMember(productVm => productVm.IsProduct,
                opt => opt.MapFrom(product => product.IsProduct))
            .ForMember(productVm => productVm.PhotoPath,
                opt => opt.MapFrom(product => product.PhotoPath))
            .ForMember(productVm => productVm.Adress,
                opt => opt.MapFrom(product => product.Adress));
    }
}