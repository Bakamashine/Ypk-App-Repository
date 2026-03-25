
using Aplication.Commands.Users.UpdateUser;
using Application.Common.Mappings;
using AutoMapper;
using Domain.Model;
using System.ComponentModel.DataAnnotations;

namespace CourseWebApi.Models.User
{
    public class UpdateUserDto : IMapWith<UpdateUserCommand>
    {
        [Required]
        public Guid Id { get; set; }
        public string OldPassword { get; set; } = string.Empty;
        public string NewPassword { get; set; } = string.Empty;
        public string Fullname { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string? UserInfo { get; set; }
        public bool IsActive { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<UpdateUserDto, UpdateUserCommand>()
                .ForMember(userCommand => userCommand.Id,
                opt => opt.MapFrom(userDto => userDto.Id))
                .ForMember(userCommand => userCommand.OldPassword,
                opt => opt.MapFrom(userDto => userDto.OldPassword))
                .ForMember(userCommand => userCommand.NewPassword,
                opt => opt.MapFrom(userDto => userDto.NewPassword))
                .ForMember(userCommand => userCommand.Fullname,
                opt => opt.MapFrom(userDto => userDto.Fullname))
                .ForMember(userCommand => userCommand.PhoneNumber,
                opt => opt.MapFrom(userDto => userDto.PhoneNumber))
                .ForMember(userCommand => userCommand.UserInfo,
                opt => opt.MapFrom(userDto => userDto.UserInfo))
                .ForMember(userCommand => userCommand.IsActive,
                opt => opt.MapFrom(userDto => userDto.IsActive));
        }
    }
}
