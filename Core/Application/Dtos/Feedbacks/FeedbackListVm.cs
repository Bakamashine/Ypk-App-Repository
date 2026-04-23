namespace Application.Dtos.Feedbacks;

public class FeedbackListVm
{
    public IList<FeedbackLookupDto> Feedbacks { get; set; } = new List<FeedbackLookupDto>();
}