using Aplication.Dtos.Users;
using Application.Common.Mappings;
using AutoMapper;
using Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplication.Dtos.Products
{
    public class ProductLookupDto : IMapWith<Product>
    {
        public Guid Id { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public decimal ProductCost { get; set; }
        public string ProductInfo { get; set; } = string.Empty;
        public bool IsProduct { get; set; }

        public string? PhotoPath { get; set; }      
        public string? PhotoUrl { get; set; }

        public string Adress { get; set; } = string.Empty;
        public decimal Raiting { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Product, ProductLookupDto>()
                .ForMember(productVm => productVm.Id,
                 opt => opt.MapFrom(product => product.Id))
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
                 opt => opt.MapFrom(product => product.Adress))
                 .ForMember(productVm => productVm.Raiting,
                 opt => opt.MapFrom(product => product.Raiting));

        }
    }

}
