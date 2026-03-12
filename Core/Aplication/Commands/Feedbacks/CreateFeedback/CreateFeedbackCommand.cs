using Domain.Model;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplication.Commands.Feedbacks.CreateFeedback
{
    public class CreateFeedbackCommand : IRequest<Guid>
    {
        public Guid CurrentUserId { get; set; }
        public string FeedbackName { get; set; } = string.Empty;
        public int Raiting { get; set; }
        public IFormFile? Image { get; set; }
    }
}
