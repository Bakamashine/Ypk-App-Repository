using Aplication.Commands.Orders.CreateOrder;
using Aplication.Commands.Orders.UpdateOrder;
using Application.Common.Mappings;
using AutoMapper;
using Domain.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductsWebApi.Models.Order
{
    public class UpdateOrderDto : IMapWith<UpdateOrderCommand>
    {
        [Required]
        public Guid Id { get; set; }

        public Domain.Model.Product? Product { get; set; }
        public StatusOrder? StatusOrder { get; set; }

        public string? CustomersComment { get; set; }
        public string? UserComment { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<UpdateOrderDto, UpdateOrderCommand>()
                .ForMember(userCommand => userCommand.Id,
                opt => opt.MapFrom(userDto => userDto.Id))
                .ForMember(userCommand => userCommand.Product,
                opt => opt.MapFrom(userDto => userDto.Product))
                .ForMember(userCommand => userCommand.StatusOrder,
                opt => opt.MapFrom(userDto => userDto.StatusOrder))
                .ForMember(userCommand => userCommand.CustomersComment,
                opt => opt.MapFrom(userDto => userDto.CustomersComment))
                .ForMember(userCommand => userCommand.UserComment,
                opt => opt.MapFrom(userDto => userDto.UserComment));
        }
    }

}
