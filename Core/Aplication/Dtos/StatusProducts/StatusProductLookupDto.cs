using Aplication.Dtos.Roles;
using Application.Common.Mappings;
using AutoMapper;
using Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplication.Dtos.StatusProducts
{
    public class StatusProductLookupDto : IMapWith<StatusProduct>
    {
        public Guid Id { get; set; }
        public string StatusName { get; set; } = string.Empty;

        public void Mapping(Profile profile) =>
            profile.CreateMap<StatusProduct, StatusProductLookupDto>()
                .ForMember(statusNameVm => statusNameVm.Id,
                opt => opt.MapFrom(statusName => statusName.Id))
                .ForMember(statusNameVm => statusNameVm.StatusName,
                opt => opt.MapFrom(statusName => statusName.StatusName));
    }
}
