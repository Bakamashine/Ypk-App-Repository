using Aplication.Commands.Feedbacks.CreateFeedback;
using Aplication.Commands.Orders.CreateOrder;
using Application.Common.Mappings;
using AutoMapper;
using Domain.Model;
using ProductsWebApi.Models.Feedback;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductsWebApi.Models.Order
{
    public class CreateOrderDto : IMapWith<CreateOrderCommand>
    {
        [Required]
        public Guid ProductId { get; set; }
        [Required]
        public Guid StatusOrderId { get; set; }

        public string? CustomersComment { get; set; }
        public string? UserComment { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<CreateOrderDto, CreateOrderCommand>()
                .ForMember(userCommand => userCommand.ProductId,
                opt => opt.MapFrom(userDto => userDto.ProductId))
                .ForMember(userCommand => userCommand.StatusOrderId,
                opt => opt.MapFrom(userDto => userDto.StatusOrderId))
                .ForMember(userCommand => userCommand.CustomersComment,
                opt => opt.MapFrom(userDto => userDto.CustomersComment))
                .ForMember(userCommand => userCommand.UserComment,
                opt => opt.MapFrom(userDto => userDto.UserComment));
        }
    }
}
