using Aplication.Commands.Feedbacks.UpdateFeedback;
using Aplication.Commands.Ypks.UpdateYpk;
using Application.Common.Mappings;
using AutoMapper;
using ProductsWebApi.Models.Ypk;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductsWebApi.Models.Feedback
{
    public class UpdateFeedbackDto : IMapWith<UpdateFeedbackCommand>
    {
        [Required]
        public Guid Id { get; set; }
        public string Comment { get; set; } = string.Empty;
        public int Raiting { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<UpdateFeedbackDto, UpdateFeedbackCommand>()
                .ForMember(userCommand => userCommand.Comment,
                opt => opt.MapFrom(userDto => userDto.Comment))
                .ForMember(userCommand => userCommand.Id,
                opt => opt.MapFrom(userDto => userDto.Id))
                .ForMember(userCommand => userCommand.Raiting,
                opt => opt.MapFrom(userDto => userDto.Raiting));
        }
    }
}
