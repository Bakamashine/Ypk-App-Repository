using Application.Common.Mappings;
using AutoMapper;
using Domain.Model;

namespace Application.Dtos.Ypks;

public class YpkLookupDto : IMapWith<Ypk>
{
    public Guid Id { get; set; }
    public string YpkName { get; set; } = string.Empty;
    public bool IsActive { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<Ypk, YpkLookupDto>()
            .ForMember(ypkVm => ypkVm.Id,
                opt => opt.MapFrom(ypk => ypk.Id))
            .ForMember(ypkVm => ypkVm.YpkName,
                opt => opt.MapFrom(ypk => ypk.YpkName))
            .ForMember(ypkVm => ypkVm.IsActive,
                opt => opt.MapFrom(ypk => ypk.IsActive));
    }
}