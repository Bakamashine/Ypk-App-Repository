using Aplication.Commands.Ypks.CreateYpk;
using Aplication.Commands.Ypks.UpdateYpk;
using Application.Common.Mappings;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductsWebApi.Models.Ypk
{
    public class UpdateYpkDto : IMapWith<UpdateYpkCommand>
    {
        [Required]
        public Guid Id { get; set; }
        public string YpkName { get; set; } = string.Empty;
        public string? Description { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<UpdateYpkDto, UpdateYpkCommand>()
                .ForMember(userCommand => userCommand.YpkName,
                opt => opt.MapFrom(userDto => userDto.YpkName))
                .ForMember(userCommand => userCommand.Id,
                opt => opt.MapFrom(userDto => userDto.Id))
                .ForMember(userCommand => userCommand.Description,
                opt => opt.MapFrom(userDto => userDto.Description));
        }
    }
}
