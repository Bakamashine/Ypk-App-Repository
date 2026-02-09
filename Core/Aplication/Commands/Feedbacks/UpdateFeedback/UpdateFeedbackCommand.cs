using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplication.Commands.Feedbacks.UpdateFeedback
{
    public class UpdateFeedbackCommand : IRequest
    {
        public Guid Id { get; set; }
        public Guid CurrentUserId { get; set; }
        public string FeedbackName { get; set; } = string.Empty;
        public int Raiting { get; set; }
    }
}
