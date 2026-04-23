using System.ComponentModel.DataAnnotations;
using Application.Commands.Ypks.UpdateYpk;
using Application.Common.Mappings;
using AutoMapper;

namespace ProductsWebApi.Models.Ypk;

public class UpdateYpkDto : IMapWith<UpdateYpkCommand>
{
    [Required] public Guid Id { get; set; }

    public string YpkName { get; set; } = string.Empty;

    public void Mapping(Profile profile)
    {
        profile.CreateMap<UpdateYpkDto, UpdateYpkCommand>()
            .ForMember(userCommand => userCommand.YpkName,
                opt => opt.MapFrom(userDto => userDto.YpkName))
            .ForMember(userCommand => userCommand.Id,
                opt => opt.MapFrom(userDto => userDto.Id));
    }
}