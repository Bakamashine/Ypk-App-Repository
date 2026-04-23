namespace Application.Dtos.Users;

public class UserListVm
{
    public IList<UserLookupDto> Users { get; set; } = new List<UserLookupDto>();
}