using Application.Commands.Users.UpdateCurrentUser;
using Application.Commands.Users.UpdateUser;
using Application.Common.Mappings;
using AutoMapper;
using System.ComponentModel.DataAnnotations;

namespace CourseWebApi.Models.User;

public class UpdateCurrentUserDto : IMapWith<UpdateCurrentUserCommand>
{

    public string Fullname { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public string? UserInfo { get; set; }
    public IFormFile? Avatar { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<UpdateCurrentUserDto, UpdateCurrentUserCommand>()
            .ForMember(userCommand => userCommand.Fullname,
                opt => opt.MapFrom(userDto => userDto.Fullname))
            .ForMember(userCommand => userCommand.PhoneNumber,
                opt => opt.MapFrom(userDto => userDto.PhoneNumber))
            .ForMember(userCommand => userCommand.UserInfo,
                opt => opt.MapFrom(userDto => userDto.UserInfo))
            .ForMember(userCommand => userCommand.Avatar,
                opt => opt.MapFrom(userDto => userDto.Avatar));
    }
}