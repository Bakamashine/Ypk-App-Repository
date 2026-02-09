using Aplication.Commands.Orders.CreateOrder;
using Aplication.Commands.Orders.DeleteOrder;
using Aplication.Commands.Orders.UpdateOrder;
using Aplication.Commands.Users.CreateUser;
using Aplication.Commands.Users.DeleteUser;
using Aplication.Commands.Users.UpdateUser;
using Aplication.Dtos.Feedbacks;
using Aplication.Dtos.Users;
using Aplication.Queries.Feedbacks.GetFeedback;
using Aplication.Queries.Feedbacks.GetFeedbackList;
using Aplication.Queries.Users.GetUser;
using Aplication.Queries.Users.GetUserList;
using AutoMapper;
using CourseWebApi.Models.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProductsWebApi.Models.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductsWebApi.Controllers
{
    [Route("api/[controller]")]
    public class UserController : BaseController
    {

        private readonly IMapper mapper;

        public UserController(IMapper mapper)
        {
            this.mapper = mapper;
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("All")]
        public async Task<ActionResult<UserListVm>> GetAll()
        {
            var query = new GetAllUserQuery()
            {

            };

            var vm = await Mediator.Send(query);
            return Ok(vm);
        }
        [Authorize(Roles = "Manager,Admin")]
        [HttpGet("{id}")]
        public async Task<ActionResult<UserLookupDto>> Get(Guid id)
        {
            var query = new GetDetailsUserQuery
            {
                Id = id,
            };
            var vm = await Mediator.Send(query);
            return Ok(vm);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<ActionResult<Guid>> Create([FromBody] CreateUserDto createUserDto)
        {
            var command = mapper.Map<CreateUserCommand>(createUserDto);
            command.CurrentUserId = UserId;
            var commandId = await Mediator.Send(command);
            return Ok(commandId);
        }
        [Authorize(Roles = "Admin")]
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateUserDto updateUserDto)
        {
            var command = mapper.Map<UpdateUserCommand>(updateUserDto);
            command.CurrentUserId = UserId;
            await Mediator.Send(command);
            return NoContent();
        }
        [Authorize(Roles = "Admin")]
        [HttpPut("admin")]
        public async Task<IActionResult> UpdateAdmin([FromBody] UpdateUserForAdminDto updateUserDto)
        {
            var command = mapper.Map<UpdateAdminUserCommand>(updateUserDto);
            command.CurrentUserId = UserId;
            await Mediator.Send(command);
            return NoContent();
        }
        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var command = new DeleteUserCommand
            {
                Id = id,
                CurrentUserId = UserId
            };

            await Mediator.Send(command);
            return NoContent();
        }
    }
}
