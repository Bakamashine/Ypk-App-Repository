using Aplication.Dtos.Products;
using Application.Common.Mappings;
using AutoMapper;
using Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplication.Dtos.Orders
{
    public class OrderLookupDto : IMapWith<Order>
    {
        public Guid Id { get; set; }
        public DateTime Date { get; set; }
        public string? CustomersComment { get; set; }
        public string? UserComment { get; set; }
        public User? User { get; set; }
        public Product? Product { get; set; }
        public StatusOrder? StatusOrder { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Order, OrderLookupDto>()
                .ForMember(productVm => productVm.Id,
                 opt => opt.MapFrom(product => product.Id))
                .ForMember(productVm => productVm.Date,
                 opt => opt.MapFrom(product => product.Date))
                .ForMember(productVm => productVm.CustomersComment,
                 opt => opt.MapFrom(product => product.CustomersComment))
                 .ForMember(productVm => productVm.User,
                 opt => opt.MapFrom(product => product.User))
                 .ForMember(productVm => productVm.UserComment,
                 opt => opt.MapFrom(product => product.UserComment))
                 .ForMember(productVm => productVm.Product,
                 opt => opt.MapFrom(product => product.Product))
                 .ForMember(productVm => productVm.StatusOrder,
                 opt => opt.MapFrom(product => product.StatusOrder));

        }
    }
}
