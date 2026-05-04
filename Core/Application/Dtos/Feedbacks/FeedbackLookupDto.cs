using Application.Common.Mappings;
using Application.Dtos.Users;
using AutoMapper;
using Domain.Model;

namespace Application.Dtos.Feedbacks;

public class FeedbackLookupDto : IMapWith<Feedback>, IHasImage
{
    public Guid Id { get; set; }
    public string Comment { get; set; } = string.Empty;
    public int Raiting { get; set; }
    public UserLookupDto? User { get; set; }

    // feedback.ImageUrl = $"{Request.Scheme}://{Request.Host}{feedback.ImagePath}";


    public string? ImagePath
    {
        get;
        set;
    }

    public string? ImageUrl { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<Feedback, FeedbackLookupDto>()
            .ForMember(feedbackVm => feedbackVm.Id,
                opt => opt.MapFrom(feedback => feedback.Id))
            .ForMember(feedbackVm => feedbackVm.Comment,
                opt => opt.MapFrom(feedback => feedback.Comment))
            .ForMember(feedbackVm => feedbackVm.Raiting,
                opt => opt.MapFrom(feedback => feedback.Raiting))
            .ForMember(feedbackVm => feedbackVm.User,
                opt => opt.MapFrom(feedback => feedback.User))
            .ForMember(dto => dto.ImagePath,
                opt => opt.MapFrom(f => f.ImagePath));
    }
}