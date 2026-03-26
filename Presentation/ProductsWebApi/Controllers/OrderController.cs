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
using Aplication.Queries.Orders.GetOrderListForDefaultUser;
using Aplication.Queries.Orders.GetOrderListForDefaultUserHistory;
using Aplication.Queries.Orders.GetOrderListForManager;
using Aplication.Queries.Orders.GetOrderListForManagerHistory;
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

        /// <summary>
        /// Get orders for manager 
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// GET (HOST)/api/order/manager
        /// </remarks>
        /// <returns>Returns OrderListVm</returns>
        /// <response code="200">Siccess</response>
        /// <response code="401">If the user is unautorized</response>
        /// <response code="403">If the user not have permission</response>
        [HttpGet("manager")]
        [Authorize(Roles = "Manager,Admin")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<OrderListVm>> GetAllForManager()
        {
            var query = new GetAllOrderQueryForManager()
            {
                CurrentUserId = UserId,
            };

            var vm = await Mediator.Send(query);
            return Ok(vm);
        }


        /// <summary>
        /// Get orders for user 
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// GET (HOST)/api/order/user
        /// </remarks>
        /// <returns>Returns OrderListVm</returns>
        /// <response code="200">Siccess</response>
        /// <response code="401">If the user is unautorized</response>
        [HttpGet("user")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<OrderListVm>> GetAllForUser()
        {
            var query = new GetAllOrderQueryForDefaultUser()
            {
                CurrentUserId = UserId,
            };

            var vm = await Mediator.Send(query);
            return Ok(vm);
        }

        /// <summary>
        /// Get all orders
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// GET (HOST)/api/order/all
        /// </remarks>
        /// <returns>Returns OrderListVm</returns>
        /// <response code="200">Siccess</response>
        /// <response code="401">If the user is unautorized</response>
        [HttpGet("all")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<OrderListVm>> GetAll()
        {
            var query = new GetAllOrderQuery()
            {
                
            };

            var vm = await Mediator.Send(query);
            return Ok(vm);
        }


        /// <summary>
        /// Get orders history for manager 
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// GET (HOST)/api/order/manager/History
        /// </remarks>
        /// <returns>Returns OrderListVm</returns>
        /// <response code="200">Siccess</response>
        /// <response code="401">If the user is unautorized</response>
        /// <response code="403">If the user not have permission</response>
        [HttpGet("manager/History")]
        [Authorize(Roles = "Manager,Admin")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<OrderListVm>> GetAllForManagerHistory()
        {
            var query = new GetAllOrderQueryForManagerHistory()
            {
                CurrentUserId = UserId,
            };

            var vm = await Mediator.Send(query);
            return Ok(vm);
        }


        /// <summary>
        /// Get orders history for user 
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// GET (HOST)/api/order/user/History
        /// </remarks>
        /// <returns>Returns OrderListVm</returns>
        /// <response code="200">Siccess</response>
        /// <response code="401">If the user is unautorized</response>
        [HttpGet("user/History")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<OrderListVm>> GetAllForUserHistory()
        {
            var query = new GetAllOrderQueryForDefaultUserHistory()
            {
                CurrentUserId = UserId,
            };

            var vm = await Mediator.Send(query);
            return Ok(vm);
        }


        /// <summary>
        /// Get info order by id
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// GET (HOST)/api/order/AB670EFA-9049-46F2-AA3F-8C5044657851
        /// </remarks>
        /// <param name="id">Order id guid</param>
        /// <returns>Returns OrderLookupDto</returns>
        /// <response code="200">Siccess</response>
        /// <response code="401">If the user is unautorized</response>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<OrderLookupDto>> Get(Guid id)
        {
            var query = new GetDetailOrderQuery
            {
                Id = id,
            };
            var vm = await Mediator.Send(query);
            return Ok(vm);
        }

        /// <summary>
        /// Create object order 
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// POST (HOST)/api/order
        /// {
        ///  "ProductId": "3aa45f64-5717-4562-b3fc-2c963f66aff2",
        ///  "UserComment": "string"
        ///  "CustomersComment": "string"
        /// }
        /// </remarks>
        /// <param name="createOrderDto">CreateOrderDto object</param>
        /// <returns>Returns id (guid)</returns>
        /// <response code="201">Siccess</response>
        /// <response code="401">If the user is unautorized</response>
        /// <response code="403">If the user not have permission</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<Guid>> Create([FromBody] CreateOrderDto createOrderDto)
        {
            var command = mapper.Map<CreateOrderCommand>(createOrderDto);
            command.CurrentUserId = UserId;
            var commandId = await Mediator.Send(command);
            return Ok(commandId);
        }


        /// <summary>
        /// Update object order 
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// PUT (HOST)/api/order
        ///{
        ///  "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
        ///  "ProductId": "3aa45f64-5717-4562-b3fc-2c963f66aff2",
        ///  "StatusOrderId": "7ab72f64-5717-4562-bgfc-2c963f663sa2"
        ///  "ExecutorId": "8сb72f64-5717-4562-bgfc-2c963f663sa2"
        ///  "UserComment": "string"
        ///  "CustomersComment": "string"
        ///}        
        /// </remarks>
        /// <param name="updateOrderDto">UpdateOrderDto object</param>
        /// <returns>Returns NoContent</returns>
        /// <response code="204">Siccess</response>
        /// <response code="401">If the user is unautorized</response>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Update([FromBody] UpdateOrderDto updateOrderDto)
        {
            var command = mapper.Map<UpdateOrderCommand>(updateOrderDto);
            command.CurrentUserId = UserId;
            await Mediator.Send(command);
            return NoContent();
        }

        /// <summary>
        /// Delete object order by id
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// DELETE (HOST)/api/order/AB670EFA-9049-46F2-A5BF-8C5044287851
        /// </remarks>
        /// <param name="id">Order id (guid)</param>
        /// <returns>Returns NoContent</returns>
        /// <response code="204">Siccess</response>
        /// <response code="401">If the user is unautorized</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
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
