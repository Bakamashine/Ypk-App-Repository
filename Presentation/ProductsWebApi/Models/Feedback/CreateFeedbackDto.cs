using Aplication.Commands.Feedbacks.CreateFeedback;
using Aplication.Commands.Ypks.CreateYpk;
using Application.Common.Mappings;
using AutoMapper;
using Domain.Model;
using MediatR;
using ProductsWebApi.Models.Ypk;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductsWebApi.Models.Feedback
{
    public class CreateFeedbackDto : IMapWith<CreateFeedbackCommand>
    {
        [Required]
        public string FeedbackName { get; set; } = string.Empty;
        [Required]
        public int Raiting { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<CreateFeedbackDto, CreateFeedbackCommand>()
                .ForMember(userCommand => userCommand.FeedbackName,
                opt => opt.MapFrom(userDto => userDto.FeedbackName))
                .ForMember(userCommand => userCommand.Raiting,
                opt => opt.MapFrom(userDto => userDto.Raiting));
        }
    }
}
