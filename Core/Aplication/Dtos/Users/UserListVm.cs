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
    public class UserListVm
    {
        public IList<UserLookupDto> Users { get; set; } = new List<UserLookupDto>();
    }
}
