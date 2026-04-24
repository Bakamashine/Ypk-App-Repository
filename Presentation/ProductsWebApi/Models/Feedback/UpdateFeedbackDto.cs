using System.ComponentModel.DataAnnotations;
using Application.Commands.Feedbacks.UpdateFeedback;
using Application.Common.Mappings;
using AutoMapper;

namespace ProductsWebApi.Models.Feedback;

public class UpdateFeedbackDto : IMapWith<UpdateFeedbackCommand>
{
    [Required] public Guid Id { get; set; }

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