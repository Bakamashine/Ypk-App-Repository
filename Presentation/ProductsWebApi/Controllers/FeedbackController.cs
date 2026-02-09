using Aplication.Commands.Feedbacks.CreateFeedback;
using Aplication.Commands.Feedbacks.DeleteFeedback;
using Aplication.Commands.Feedbacks.UpdateFeedback;
using Aplication.Dtos.Feedbacks;
using Aplication.Queries.Feedbacks.GetFeedback;
using Aplication.Queries.Feedbacks.GetFeedbackList;
using AutoMapper;
using Domain.Model;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProductsWebApi.Models.Feedback;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductsWebApi.Controllers
{
    
    [Route("api/[controller]")]
    public class FeedbackController : BaseController
    {
        private readonly IMapper mapper;

        public FeedbackController(IMapper mapper)
        {
            this.mapper = mapper;
        }

        [HttpGet("All")]
        public async Task<ActionResult<FeedbackListVm>> GetAll()
        {
            var query = new GetAllFeedbackQuery()
            {

            };

            var vm = await Mediator.Send(query);
            return Ok(vm);
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<FeedbackLookupDto>> Get(Guid id)
        {
            var query = new GetDetailsFeedbackQuery
            {
                Id = id,
            };
            var vm = await Mediator.Send(query);
            return Ok(vm);
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult<Guid>> Create([FromBody] CreateFeedbackDto createFeedbackDto)
        {
            var command = mapper.Map<CreateFeedbackCommand>(createFeedbackDto);
            command.CurrentUserId = UserId;
            var commandId = await Mediator.Send(command);
            return Ok(commandId);
        }

        [Authorize]
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateFeedbackDto updateFeedbackDto)
        {
            var command = mapper.Map<UpdateFeedbackCommand>(updateFeedbackDto);
            command.CurrentUserId = UserId;
            await Mediator.Send(command);
            return NoContent();
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var command = new DeleteFeedbackCommand
            {
                Id = id,
                CurrentUserId = UserId
            };

            await Mediator.Send(command);
            return NoContent();
        }
    }
}
