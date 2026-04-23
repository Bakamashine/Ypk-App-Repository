namespace Application.Dtos.Ypks;

public class YpkListVm
{
    public IList<YpkLookupDto> Ypks { get; set; } = new List<YpkLookupDto>();
}