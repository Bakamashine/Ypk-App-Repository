using System.ComponentModel.DataAnnotations;
using Application.Commands.Products.UpdateProduct;
using Application.Common.Mappings;
using AutoMapper;

namespace ProductsWebApi.Models.Product;

public class UpdateProductDto : IMapWith<UpdateProductCommand>
{
    [Required] public Guid Id { get; set; }

    public string ProductName { get; set; } = string.Empty;
    public string ProductInfo { get; set; } = string.Empty;
    public decimal ProductCost { get; set; }
    public bool IsProduct { get; set; }
    public string Address { get; set; } = string.Empty;
    public IFormFile? Photo { get; set; }

    public Guid YpkId { get; set; }
    public Guid StatusProductId { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<UpdateProductDto, UpdateProductCommand>()
            .ForMember(userCommand => userCommand.Id,
                opt => opt.MapFrom(userDto => userDto.Id))
            .ForMember(userCommand => userCommand.ProductName,
                opt => opt.MapFrom(userDto => userDto.ProductName))
            .ForMember(userCommand => userCommand.ProductInfo,
                opt => opt.MapFrom(userDto => userDto.ProductInfo))
            .ForMember(userCommand => userCommand.ProductCost,
                opt => opt.MapFrom(userDto => userDto.ProductCost))
            .ForMember(userCommand => userCommand.IsProduct,
                opt => opt.MapFrom(userDto => userDto.IsProduct))
            .ForMember(userCommand => userCommand.Address,
                opt => opt.MapFrom(userDto => userDto.Address))
            .ForMember(userCommand => userCommand.Photo,
                opt => opt.MapFrom(userDto => userDto.Photo))
            .ForMember(userCommand => userCommand.YpkId,
                opt => opt.MapFrom(userDto => userDto.YpkId))
            .ForMember(userCommand => userCommand.StatusProductId,
                opt => opt.MapFrom(userDto => userDto.StatusProductId));
    }
}