using System.ComponentModel.DataAnnotations;
using Application.Commands.Feedbacks.CreateFeedback;
using Application.Common.Mappings;
using AutoMapper;

namespace ProductsWebApi.Models.Feedback;

public class CreateFeedbackDto : IMapWith<CreateFeedbackCommand>
{
    [Required] public string Comment { get; set; } = string.Empty;

    [Required] public int Raiting { get; set; }

    public IFormFile? Image { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<CreateFeedbackDto, CreateFeedbackCommand>()
            .ForMember(userCommand => userCommand.Comment,
                opt => opt.MapFrom(userDto => userDto.Comment))
            .ForMember(userCommand => userCommand.Raiting,
                opt => opt.MapFrom(userDto => userDto.Raiting))
            .ForMember(cmd => cmd.Image,
                opt => opt.MapFrom(dto => dto.Image));
    }
}