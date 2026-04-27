using System.ComponentModel.DataAnnotations;
using Application.Commands.Products.CreateProduct;
using Application.Common.Mappings;
using AutoMapper;

namespace ProductsWebApi.Models.Product;

public class CreateProductDto : IMapWith<CreateProductCommand>
{
    [Required] public string ProductName { get; set; } = string.Empty;

    [Required] public string ProductInfo { get; set; } = string.Empty;

    [Required] public decimal ProductCost { get; set; }

    [Required] public bool IsProduct { get; set; }

    [Required] public string Adres { get; set; } = string.Empty;

    public IFormFile? Photo { get; set; }

    [Required] public Guid YpkId { get; set; }

    [Required] public Guid StatusProductId { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<CreateProductDto, CreateProductCommand>()
            .ForMember(userCommand => userCommand.ProductName,
                opt => opt.MapFrom(userDto => userDto.ProductName))
            .ForMember(userCommand => userCommand.ProductInfo,
                opt => opt.MapFrom(userDto => userDto.ProductInfo))
            .ForMember(userCommand => userCommand.ProductCost,
                opt => opt.MapFrom(userDto => userDto.ProductCost))
            .ForMember(userCommand => userCommand.IsProduct,
                opt => opt.MapFrom(userDto => userDto.IsProduct))
            .ForMember(userCommand => userCommand.Adres,
                opt => opt.MapFrom(userDto => userDto.Adres))
            .ForMember(userCommand => userCommand.Photo,
                opt => opt.MapFrom(userDto => userDto.Photo))
            .ForMember(userCommand => userCommand.YpkId,
                opt => opt.MapFrom(userDto => userDto.YpkId))
            .ForMember(userCommand => userCommand.StatusProductId,
                opt => opt.MapFrom(userDto => userDto.StatusProductId));
    }
}