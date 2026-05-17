using Application.Dtos.StatusProducts;
using Application.Queries.Base;
using Application.Queries.StatusProducts.GetStatusProductsList;
using Application.Queries.StatusProducts.GetStatusProductPagList;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ProductsWebApi.Controllers;

[Route("api/[controller]")]
public class StatusProductController : BaseController
{
    /// <summary>
    ///     Get all status product
    /// </summary>
    /// <remarks>
    ///     Sample request:
    ///     GET (HOST)/api/StatusProduct/all
    /// </remarks>
    /// <returns>Returns StatusProductListVm</returns>
    /// <response code="200">Success</response>
    /// <response code="401">If the user is unauthorized</response>
    /// <response code="403">If the user not have permission</response>
    [Authorize]
    [HttpGet("all")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<ActionResult<StatusProductListVm>> GetAll()
    {
        var query = new GetAllStatusProductQuery();

        var vm = await Mediator.Send(query);
        return Ok(vm);
    }

    [Authorize]
    [HttpGet("pag/all")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<ActionResult<PagedList<StatusProductLookupDto>>> GetAllWithPag(
        [FromQuery] int pageSize = 5,
        [FromQuery] int page = 1
    )
    {
        var query = new GetAllStatusProductPagQuery { PageSize = pageSize, Page = page };

        var vm = await Mediator.Send(query);
        return Ok(vm);
    }
}