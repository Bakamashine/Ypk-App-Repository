using System.ComponentModel.DataAnnotations;
using Application.Commands.Ypks.CreateYpk;
using Application.Common.Mappings;
using AutoMapper;

namespace ProductsWebApi.Models.Ypk;

public class CreateYpkDto : IMapWith<CreateYpkCommand>
{
    [Required] public string YpkName { get; set; } = string.Empty;
    public string? Description { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<CreateYpkDto, CreateYpkCommand>()
            .ForMember(userCommand => userCommand.YpkName,
                opt => opt.MapFrom(userDto => userDto.YpkName))
            .ForMember(userCommand => userCommand.Description,
                opt => opt.MapFrom(userDto => userDto.Description));
    }
}