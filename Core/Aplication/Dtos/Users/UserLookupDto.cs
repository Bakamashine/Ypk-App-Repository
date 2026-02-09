using Aplication.Dtos.Roles;
using Application.Common.Mappings;
using AutoMapper;
using Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplication.Dtos.Users
{
    public class UserLookupDto : IMapWith<User>
    {
        public Guid Id { get; set; }
        public string Fullname { get; set; } = string.Empty;
        public string HashPassword { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string? UserInfo { get; set; }
        public bool IsActive{ get; set; }
        public Role? Role { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<User, UserLookupDto>()
                .ForMember(userVm => userVm.Id,
                 opt => opt.MapFrom(user => user.Id))
                .ForMember(userVm => userVm.Fullname,
                 opt => opt.MapFrom(user => user.Fullname))
                .ForMember(userVm => userVm.Role,
                 opt => opt.MapFrom(user => user.Role))
                 .ForMember(userVm => userVm.PhoneNumber,
                 opt => opt.MapFrom(user => user.PhoneNumber))
                 .ForMember(userVm => userVm.HashPassword,
                 opt => opt.MapFrom(user => user.HashPassword))
                 .ForMember(userVm => userVm.UserInfo,
                 opt => opt.MapFrom(user => user.UserInfo))
                 .ForMember(userVm => userVm.IsActive,
                 opt => opt.MapFrom(user => user.IsActive));
                
        }
    }
}
