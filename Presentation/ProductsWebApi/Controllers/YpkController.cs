using Aplication.Commands.Orders.CreateOrder;
using Aplication.Commands.Orders.DeleteOrder;
using Aplication.Commands.Orders.UpdateOrder;
using Aplication.Commands.Ypks.CreateYpk;
using Aplication.Commands.Ypks.DeleteYpk;
using Aplication.Commands.Ypks.UpdateYpk;
using Aplication.Dtos.Feedbacks;
using Aplication.Dtos.Ypks;
using Aplication.Queries.Feedbacks.GetFeedback;
using Aplication.Queries.Feedbacks.GetFeedbackList;
using Aplication.Queries.Ypks.GetYpk;
using Aplication.Queries.Ypks.GetYpkList;
using AutoMapper;
using Domain.Model;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProductsWebApi.Models.Order;
using ProductsWebApi.Models.Ypk;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductsWebApi.Controllers
{
    [Route("api/[controller]")]
    public class YpkController : BaseController
    {
        private readonly IMapper mapper;

        public YpkController(IMapper mapper)
        {
            this.mapper = mapper;
        }

        [HttpGet("All")]
        public async Task<ActionResult<YpkListVm>> GetAll()
        {
            var query = new GetAllYpkQuery()
            {

            };

            var vm = await Mediator.Send(query);
            return Ok(vm);
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<YpkLookupDto>> Get(Guid id)
        {
            var query = new GetDetailsYpkQuery
            {
                Id = id,
            };
            var vm = await Mediator.Send(query);
            return Ok(vm);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<ActionResult<Guid>> Create([FromBody] CreateYpkDto createYpkDto)
        {
            var command = mapper.Map<CreateYpkCommand>(createYpkDto);
            var commandId = await Mediator.Send(command);
            return Ok(commandId);
        }

        [Authorize(Roles = "Manager,Admin")]
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateYpkDto updateYpkDto)
        {
            var command = mapper.Map<UpdateYpkCommand>(updateYpkDto);
            command.CurrentUserId = UserId;
            await Mediator.Send(command);
            return NoContent();
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var command = new DeleteYpkCommand
            {
                Id = id,
                CurrentUserId = UserId
            };

            await Mediator.Send(command);
            return NoContent();
        }
    }
}
