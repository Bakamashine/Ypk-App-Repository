using Aplication.Dtos.StatusProducts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplication.Dtos.Feedbacks
{
    public class FeedbackListVm
    {
        public IList<FeedbackLookupDto> Feedbacks { get; set; } = new List<FeedbackLookupDto>();
    }
}
