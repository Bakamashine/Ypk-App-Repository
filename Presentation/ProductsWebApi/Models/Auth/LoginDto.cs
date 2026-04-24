using System.ComponentModel.DataAnnotations;
using Application.Commands.Auth.Login;
using Application.Common.Mappings;
using AutoMapper;

namespace CourseWebApi.Models.Auth;

public class LoginDto : IMapWith<LoginUserCommand>
{
    [Required] public string PhoneNumber { get; set; } = string.Empty;

    [Required] public string Password { get; set; } = string.Empty;

    public void Mapping(Profile profile)
    {
        profile.CreateMap<LoginDto, LoginUserCommand>()
            .ForMember(loginCm => loginCm.PhoneNumber, opt =>
                opt.MapFrom(loginDto => loginDto.PhoneNumber))
            .ForMember(loginCm => loginCm.Password, opt =>
                opt.MapFrom(loginDto => loginDto.Password));
    }
}