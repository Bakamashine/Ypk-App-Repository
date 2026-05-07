using System.ComponentModel.DataAnnotations;
using Application.Commands.Users.UpdateUser;
using Application.Common.Mappings;
using AutoMapper;

namespace CourseWebApi.Models.User;

public class UpdateUserForAdminDto : IMapWith<UpdateAdminUserCommand>
{
    [Required] public Guid Id { get; set; }

    public string Fullname { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public Guid RoleId { get; set; }
    public string? UserInfo { get; set; }
    public Guid? YpkId { get; set; }
    public IFormFile? Avatar { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<UpdateUserForAdminDto, UpdateAdminUserCommand>()
            .ForMember(userCommand => userCommand.Id,
                opt => opt.MapFrom(userDto => userDto.Id))
            .ForMember(userCommand => userCommand.Fullname,
                opt => opt.MapFrom(userDto => userDto.Fullname))
            .ForMember(userCommand => userCommand.RoleId,
                opt => opt.MapFrom(userDto => userDto.RoleId))
            .ForMember(userCommand => userCommand.YpkId,
                opt => opt.MapFrom(userDto => userDto.YpkId))
            .ForMember(userCommand => userCommand.PhoneNumber,
                opt => opt.MapFrom(userDto => userDto.PhoneNumber))
            .ForMember(userCommand => userCommand.Avatar,
                opt => opt.MapFrom(userDto => userDto.Avatar));
    }
}