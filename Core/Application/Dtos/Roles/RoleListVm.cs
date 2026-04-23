namespace Application.Dtos.Roles;

public class RoleListVm
{
    public IList<RoleLookupDto> Roles { get; set; } = new List<RoleLookupDto>();
}