using Aplication.Commands.Feedbacks.CreateFeedback;
using Aplication.Commands.Feedbacks.DeleteFeedback;
using Aplication.Commands.Feedbacks.UpdateFeedback;
using Aplication.Commands.Orders.CreateOrder;
using Aplication.Commands.Orders.DeleteOrder;
using Aplication.Commands.Orders.UpdateOrder;
using Aplication.Dtos.Feedbacks;
using Aplication.Dtos.Orders;
using Aplication.Queries.Feedbacks.GetFeedback;
using Aplication.Queries.Feedbacks.GetFeedbackList;
using Aplication.Queries.Orders.GetOrder;
using Aplication.Queries.Orders.GetOrderList;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProductsWebApi.Models.Feedback;
using ProductsWebApi.Models.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductsWebApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    public class OrderController : BaseController
    {
        private readonly IMapper mapper;

        public OrderController(IMapper mapper)
        {
            this.mapper = mapper;
        }
        [HttpGet("All")]
        public async Task<ActionResult<OrderListVm>> GetAll()
        {
            var query = new GetAllOrderQuery()
            {

            };

            var vm = await Mediator.Send(query);
            return Ok(vm);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<OrderLookupDto>> Get(Guid id)
        {
            var query = new GetDetailOrderQuery
            {
                Id = id,
            };
            var vm = await Mediator.Send(query);
            return Ok(vm);
        }
        [HttpPost]
        public async Task<ActionResult<Guid>> Create([FromBody] CreateOrderDto createOrderDto)
        {
            var command = mapper.Map<CreateOrderCommand>(createOrderDto);
            command.CurrentUserId = UserId;
            var commandId = await Mediator.Send(command);
            return Ok(commandId);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateOrderDto updateOrderDto)
        {
            var command = mapper.Map<UpdateOrderCommand>(updateOrderDto);
            command.CurrentUserId = UserId;
            await Mediator.Send(command);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var command = new DeleteOrderCommand
            {
                Id = id,
                CurrentUserId = UserId
            };

            await Mediator.Send(command);
            return NoContent();
        }
    }
}
