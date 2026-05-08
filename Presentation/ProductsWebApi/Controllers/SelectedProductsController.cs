using Application.Commands.Orders.CreateOrder;
using Application.Commands.Orders.DeleteOrder;
using Application.Commands.SelectedProductsFolder.CreateSelectedProducts;
using Application.Commands.SelectedProductsFolder.DeleteSelectedProducts;
using Application.Dtos.Orders;
using Application.Dtos.SelectedProductsFolder;
using Application.Queries.Orders.GetOrderListForManager;
using Application.Queries.SelectedProductsFolder.GetSelectedProductsList;
using Application.Queries.SelectedProductsFolder.GetSelectedProductsPagList;
using AutoMapper;
using Domain.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProductsWebApi.Models.Order;
using ProductsWebApi.Models.SelectedProducts;

namespace ProductsWebApi.Controllers
{
    [Route("api/[controller]")]
    public class SelectedProductsController : BaseController
    {
        private readonly IMapper mapper;

        public SelectedProductsController(IMapper mapper)
        {
            this.mapper = mapper;
        }

        /// <summary>
        ///     Get SelectProducts for user
        /// </summary>
        /// <remarks>
        ///     Sample request:
        ///     GET (HOST)/api/SelectedProducts/all
        /// </remarks>
        /// <returns>Returns SelectedProductsListVm</returns>
        /// <response code="200">Success</response>
        /// <response code="401">If the user is unauthorized</response>
        [HttpGet("all")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<SelectedProductsListVm>> GetAllSelectProductsForUser()
        {
            var query = new GetAllSelectedProductsQuery
            {
                CurrentUserId = UserId
            };

            var vm = await Mediator.Send(query);
            return Ok(vm);
        }

        /// <summary>
        ///     Get SelectProducts for user with pagination
        /// </summary>
        /// <remarks>
        ///     Sample request:
        ///     GET (HOST)/api/SelectedProducts/pag/all
        /// </remarks>
        /// <returns>Returns SelectedProductsListVm</returns>
        /// <response code="200">Success</response>
        /// <response code="401">If the user is unauthorized</response>
        [HttpGet("pag/all")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<SelectedProductsListVm>> GetAllSelectProductsForUserWithPag()
        {
            var query = new GetAllSelectedProductsPagQuery
            {
                CurrentUserId = UserId
            };

            var vm = await Mediator.Send(query);
            return Ok(vm);
        }

        /// <summary>
        ///     Delete object SelectProducts by id
        /// </summary>
        /// <remarks>
        ///     Sample request:
        ///     DELETE (HOST)/api/SelectProducts/AB670EFA-9049-46F2-A5BF-8C5044287851
        /// </remarks>
        /// <param name="id">SelectProducts id (guid)</param>
        /// <returns>Returns NoContent</returns>
        /// <response code="204">Success</response>
        /// <response code="401">If the user is unauthorized</response>
        [HttpDelete("{id}")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Delete(Guid id)
        {
            var command = new DeleteSelectedProductsCommand
            {
                SelectedProductId = id,
                CurrentUserId = UserId
            };

            await Mediator.Send(command);
            return NoContent();
        }

        /// <summary>
        ///     Create object SelectProducts
        /// </summary>
        /// <remarks>
        ///     Sample request:
        ///     POST (HOST)/api/SelectProducts
        ///     {
        ///     "ProductId": "3aa45f64-5717-4562-b3fc-2c963f66aff2"
        ///     }
        /// </remarks>
        /// <param name="createSelectedProductsDto">CreateOrderDto object</param>
        /// <returns>Returns id (guid)</returns>
        /// <response code="201">Success</response>
        /// <response code="401">If the user is unauthorized</response>
        [HttpPost]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<Guid>> Create([FromBody] CreateSelectedProductsDto createSelectedProductsDto)
        {
            var command = mapper.Map<CreateSelectedProductsCommand>(createSelectedProductsDto);
            command.CurrentUserId = UserId;
            var commandId = await Mediator.Send(command);
            return Ok(commandId);
        }
    }
}
