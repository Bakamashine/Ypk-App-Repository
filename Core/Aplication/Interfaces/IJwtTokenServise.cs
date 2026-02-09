using Aplication.Dtos.Auth;
using Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplication.Interfaces
{
    public interface IJwtTokenServise
    {
        public Task<TokensDto> GenerateToken(User user);
    }
}
