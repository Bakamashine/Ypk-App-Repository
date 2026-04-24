using Application.Commands.Ypks.CreateYpk;
using Application.Commands.Ypks.DeleteYpk;
using Application.Commands.Ypks.UpdateYpk;
using Application.Dtos.Ypks;
using Application.Queries.Ypks.GetYpk;
using Application.Queries.Ypks.GetYpkList;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProductsWebApi.Models.Ypk;

namespace ProductsWebApi.Controllers;

[Route("api/[controller]")]
public class YpkController : BaseController
{
    private readonly IMapper mapper;

    public YpkController(IMapper mapper)
    {
        this.mapper = mapper;
    }

    /// <summary>
    ///     Get all ypk
    /// </summary>
    /// <remarks>
    ///     Sample request:
    ///     GET (HOST)/api/ypk/all
    /// </remarks>
    /// <returns>Returns YpkListVm</returns>
    /// <response code="200">Success</response>
    [HttpGet("All")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<YpkListVm>> GetAll()
    {
        var query = new GetAllYpkQuery();

        var vm = await Mediator.Send(query);
        return Ok(vm);
    }

    /// <summary>
    ///     Get info ypk by id
    /// </summary>
    /// <remarks>
    ///     Sample request:
    ///     GET (HOST)/api/ypk/AB670EFA-9049-46F2-AA3F-8C5044657851
    /// </remarks>
    /// <param name="id">Ypk id guid</param>
    /// <returns>Returns YpkLookupDto</returns>
    /// <response code="200">Siccess</response>
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<YpkLookupDto>> Get(Guid id)
    {
        var query = new GetDetailsYpkQuery
        {
            Id = id,
            CurrentUserId = UserId
        };
        var vm = await Mediator.Send(query);
        return Ok(vm);
    }

    /// <summary>
    ///     Create object ypk
    /// </summary>
    /// <remarks>
    ///     Sample request:
    ///     POST (HOST)/api/ypk
    ///     {
    ///     "ypkName": "string",
    ///     }
    /// </remarks>
    /// <param name="createYpkDto">CreateYpkDto object</param>
    /// <returns>Returns id (guid)</returns>
    /// <response code="201">Success</response>
    /// <response code="401">If the user is unauthorized</response>
    /// <response code="403">If the user not have permission</response>
    [Authorize(Roles = "Admin")]
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<ActionResult<Guid>> Create([FromBody] CreateYpkDto createYpkDto)
    {
        var command = mapper.Map<CreateYpkCommand>(createYpkDto);
        var commandId = await Mediator.Send(command);
        return Ok(commandId);
    }


    /// <summary>
    ///     Update object ypk
    /// </summary>
    /// <remarks>
    ///     Sample request:
    ///     PUT (HOST)/api/ypk
    ///     {
    ///     "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
    ///     "YpkName": "string",
    ///     }
    /// </remarks>
    /// <param name="updateYpkDto">UpdateYpkDto object</param>
    /// <returns>Returns NoContent</returns>
    /// <response code="204">Success</response>
    /// <response code="401">If the user is unauthorized</response>
    /// <response code="403">If the user not have permission</response>
    [Authorize(Roles = "Manager,Admin")]
    [HttpPut]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<IActionResult> Update([FromBody] UpdateYpkDto updateYpkDto)
    {
        var command = mapper.Map<UpdateYpkCommand>(updateYpkDto);
        command.CurrentUserId = UserId;
        await Mediator.Send(command);
        return NoContent();
    }

    /// <summary>
    ///     Delete object ypk by id
    /// </summary>
    /// <remarks>
    ///     Sample request:
    ///     DELETE (HOST)/api/ypk/AB670EFA-9049-46F2-A5BF-8C5044287851
    /// </remarks>
    /// <param name="id">Ypk id (guid)</param>
    /// <returns>Returns NoContent</returns>
    /// <response code="204">Success</response>
    /// <response code="401">If the user is unauthorized</response>
    /// <response code="403">If the user not have permission</response>
    [Authorize(Roles = "Admin")]
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
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