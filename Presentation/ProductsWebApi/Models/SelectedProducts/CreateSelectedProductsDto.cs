using Application.Commands.SelectedProductsFolder.CreateSelectedProducts;
using Application.Commands.Users.CreateUser;
using Application.Common.Mappings;
using AutoMapper;
using CourseWebApi.Models.User;
using System.ComponentModel.DataAnnotations;

namespace ProductsWebApi.Models.SelectedProducts
{
    public class CreateSelectedProductsDto : IMapWith<CreateSelectedProductsCommand>
    {
        [Required] public Guid ProductId { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<CreateSelectedProductsDto, CreateSelectedProductsCommand>()
                       .ForMember(userCm => userCm.ProductId,
                           opt => opt.MapFrom(userDto => userDto.ProductId));
        }
    }
}
