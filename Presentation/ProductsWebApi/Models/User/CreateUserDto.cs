using Aplication.Commands.Users.CreateUser;
using Application.Common.Mappings;
using AutoMapper;
using Domain.Model;
using System.ComponentModel.DataAnnotations;

namespace CourseWebApi.Models.User
{
    public class CreateUserDto : IMapWith<CreateUserCommand>
    {
        [Required]
        public string Fullname { get; set; } = string.Empty;
        [Required]
        public string Password { get; set; } = string.Empty;
        [Required]
        public string PhoneNumber { get; set; } = string.Empty;
        public string? UserInfo { get; set; }
        [Required]
        public Guid RoleId { get; set; } = new();

        public void Mapping(Profile profile)
        {
            profile.CreateMap<CreateUserDto, CreateUserCommand>()
                .ForMember(userCm => userCm.Fullname,
                opt => opt.MapFrom(userDto => userDto.Fullname))
                .ForMember(userCm => userCm.Password,
                opt => opt.MapFrom(userDto => userDto.Password))
                .ForMember(userCm => userCm.PhoneNumber,
                opt => opt.MapFrom(userDto => userDto.PhoneNumber))
                .ForMember(userCm => userCm.UserInfo,
                opt => opt.MapFrom(userDto => userDto.UserInfo))
                .ForMember(userCm => userCm.RoleId,
                opt => opt.MapFrom(userDto => userDto.RoleId));
        }
    }
}
