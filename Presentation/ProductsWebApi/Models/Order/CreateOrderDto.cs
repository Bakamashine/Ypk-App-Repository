using System.ComponentModel.DataAnnotations;
using Application.Commands.Orders.CreateOrder;
using Application.Common.Mappings;
using AutoMapper;

namespace ProductsWebApi.Models.Order;

public class CreateOrderDto : IMapWith<CreateOrderCommand>
{
    [Required] public Guid ProductId { get; set; }

    public string? CustomersComment { get; set; }
    public string? UserComment { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<CreateOrderDto, CreateOrderCommand>()
            .ForMember(userCommand => userCommand.ProductId,
                opt => opt.MapFrom(userDto => userDto.ProductId))
            .ForMember(userCommand => userCommand.CustomersComment,
                opt => opt.MapFrom(userDto => userDto.CustomersComment))
            .ForMember(userCommand => userCommand.UserComment,
                opt => opt.MapFrom(userDto => userDto.UserComment));
    }
}