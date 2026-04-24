using System.ComponentModel.DataAnnotations;
using Application.Commands.Auth.Registration;
using Application.Common.Mappings;
using AutoMapper;

namespace CourseWebApi.Models.Auth;

public class RegistrationDto : IMapWith<RegistrationUserCommand>
{
    [Required] public string FullName { get; set; } = string.Empty;

    [Required] public string PhoneNumber { get; set; } = string.Empty;

    [Required] public string Password { get; set; } = string.Empty;

    public void Mapping(Profile profile)
    {
        profile.CreateMap<RegistrationDto, RegistrationUserCommand>()
            .ForMember(userCm => userCm.FullName,
                opt => opt.MapFrom(userDto => userDto.FullName))
            .ForMember(userCm => userCm.Password,
                opt => opt.MapFrom(userDto => userDto.Password))
            .ForMember(userCm => userCm.PhoneNumber,
                opt => opt.MapFrom(userDto => userDto.PhoneNumber));
    }
}