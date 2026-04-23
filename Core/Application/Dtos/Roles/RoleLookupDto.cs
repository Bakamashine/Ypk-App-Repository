using Application.Common.Mappings;
using AutoMapper;
using Domain.Model;

namespace Application.Dtos.Roles;

public class RoleLookupDto : IMapWith<Role>
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;

    public void Mapping(Profile profile)
    {
        profile.CreateMap<Role, RoleLookupDto>()
            .ForMember(roleVm => roleVm.Id,
                opt => opt.MapFrom(role => role.Id))
            .ForMember(roleVm => roleVm.Name,
                opt => opt.MapFrom(role => role.RoleName));
    }
}