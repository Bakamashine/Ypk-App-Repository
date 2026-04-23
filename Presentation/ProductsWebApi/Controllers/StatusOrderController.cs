using Application.Dtos.StatusOrders;
using Application.Queries.StatusOrders.GetStatusOrdersList;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ProductsWebApi.Controllers;

[Route("api/[controller]")]
public class StatusOrderController : BaseController
{
    /// <summary>
    ///     Get all status order
    /// </summary>
    /// <remarks>
    ///     Sample request:
    ///     GET (HOST)/api/StatusOrder/all
    /// </remarks>
    /// <returns>Returns StatusOrderListVm</returns>
    /// <response code="200">Success</response>
    /// <response code="401">If the user is unauthorized</response>
    /// <response code="403">If the user not have permission</response>
    [Authorize(Roles = "Manager,Admin")]
    [HttpGet("All")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<ActionResult<StatusOrderListVm>> GetAll()
    {
        var query = new GetAllStatusOrdersQuery();

        var vm = await Mediator.Send(query);
        return Ok(vm);
    }
}