using Application.Common.Mappings;
using AutoMapper;
using Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplication.Dtos.Roles
{
    public class RoleLookupDto : IMapWith<Role>
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;

        public void Mapping(Profile profile) =>
            profile.CreateMap<Role, RoleLookupDto>()
                .ForMember(roleVm => roleVm.Id,
                opt => opt.MapFrom(role => role.Id))
                .ForMember(roleVm => roleVm.Name,
                opt => opt.MapFrom(role => role.RoleName));
    }
}
