using Aplication.Dtos.Feedbacks;
using Aplication.Dtos.Roles;
using Aplication.Queries.Feedbacks.GetFeedback;
using Aplication.Queries.Feedbacks.GetFeedbackList;
using Application.Common.Queries.Roles.GetRole;
using Application.Common.Queries.Roles.GetRoleList;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductsWebApi.Controllers
{
    [Route("api/[controller]")]
    [Authorize(Roles = "Admin")]
    public class RoleController : BaseController
    {
        [HttpGet("All")]
        public async Task<ActionResult<RoleListVm>> GetAll()
        {
            var query = new GetAllRoleQuery()
            {

            };

            var vm = await Mediator.Send(query);
            return Ok(vm);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<RoleLookupDto>> Get(Guid id)
        {
            var query = new GetDetailsRoleQuery
            {
                Id = id,
            };
            var vm = await Mediator.Send(query);
            return Ok(vm);
        }
    }
}

