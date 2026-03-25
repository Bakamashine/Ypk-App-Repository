using Aplication.Dtos.StatusProducts;
using Application.Common.Mappings;
using AutoMapper;
using Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplication.Dtos.Feedbacks
{
    public class FeedbackLookupDto : IMapWith<Feedback>
    {
        public Guid Id { get; set; }
        public string Comment { get; set; } = string.Empty;


        public string? ImagePath { get; set; }  
        public string? ImageUrl { get; set; }  
        public void Mapping(Profile profile) =>
            profile.CreateMap<Feedback, FeedbackLookupDto>()
                .ForMember(feedbackVm => feedbackVm.Id,
                opt => opt.MapFrom(feedback => feedback.Id))
                .ForMember(feedbackVm => feedbackVm.Comment,
                opt => opt.MapFrom(feedback => feedback.Comment))
                .ForMember(dto => dto.ImagePath,
                opt => opt.MapFrom(f => f.ImagePath));
    }
}
