using Aplication.Commands.Orders.UpdateOrder;
using Aplication.Commands.Products.UpdateProduct;
using Application.Common.Mappings;
using AutoMapper;
using Domain.Model;
using ProductsWebApi.Models.Order;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductsWebApi.Models.Product
{
    public class UpdateProductDto : IMapWith<UpdateProductCommand>
    {
        [Required]
        public Guid Id { get; set; }

        public string ProductName { get; set; } = string.Empty;
        public string ProductInfo { get; set; } = string.Empty;
        public decimal ProductCost { get; set; }
        public bool IsProduct { get; set; }
        public string Adress { get; set; } = string.Empty;
        public string? Photo { get; set; }

        public Domain.Model.Ypk? Ypk { get; set; }
        public StatusProduct? StatusProduct { get; set; }
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
                .ForMember(userCommand => userCommand.Adress,
                opt => opt.MapFrom(userDto => userDto.Adress))
                .ForMember(userCommand => userCommand.Photo,
                opt => opt.MapFrom(userDto => userDto.Photo))
                .ForMember(userCommand => userCommand.Ypk,
                opt => opt.MapFrom(userDto => userDto.Ypk))
                .ForMember(userCommand => userCommand.StatusProduct,
                opt => opt.MapFrom(userDto => userDto.StatusProduct));
        }
    }
}
