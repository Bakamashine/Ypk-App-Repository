using Aplication.Commands.Orders.CreateOrder;
using Aplication.Commands.Products.CreateProduct;
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
    public class CreateProductDto : IMapWith<CreateProductCommand>
    {
        [Required]
        public string ProductName { get; set; } = string.Empty;
        [Required]
        public string ProductInfo { get; set; } = string.Empty;
        [Required]
        public decimal ProductCost { get; set; }
        [Required]
        public bool IsProduct { get; set; }
        [Required]
        public string Adress { get; set; } = string.Empty;
        [Required]
        public decimal Raiting { get; set; }
        public string? Photo { get; set; }

        [Required]
        public Domain.Model.Ypk? Ypk { get; set; }
        [Required]
        public StatusProduct? StatusProduct { get; set; }

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
                .ForMember(userCommand => userCommand.Adress,
                opt => opt.MapFrom(userDto => userDto.Adress))
                .ForMember(userCommand => userCommand.Photo,
                opt => opt.MapFrom(userDto => userDto.Photo))
                .ForMember(userCommand => userCommand.Ypk,
                opt => opt.MapFrom(userDto => userDto.Ypk))
                .ForMember(userCommand => userCommand.Raiting,
                opt => opt.MapFrom(userDto => userDto.Raiting))
                .ForMember(userCommand => userCommand.StatusProduct,
                opt => opt.MapFrom(userDto => userDto.StatusProduct));
        }
    }
}
