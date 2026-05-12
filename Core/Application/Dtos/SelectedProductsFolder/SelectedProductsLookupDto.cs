using Application.Common.Mappings;
using Application.Dtos.StatusOrders;
using AutoMapper;
using Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dtos.SelectedProductsFolder
{
    public class SelectedProductsLookupDto : IMapWith<SelectedProducts>
    {
        public Guid Id { get; set; }
        public Product Product { get; set; }
        public User User { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<SelectedProducts, SelectedProductsLookupDto>()
                .ForMember(selectedProductsVm => selectedProductsVm.Id,
                    opt => opt.MapFrom(selectedProducts => selectedProducts.Id))
                .ForMember(selectedProductsVm => selectedProductsVm.Product,
                    opt => opt.MapFrom(selectedProducts => selectedProducts.Product))
                .ForMember(selectedProductsVm => selectedProductsVm.User,
                    opt => opt.MapFrom(selectedProducts => selectedProducts.User));
        }
    }
}
