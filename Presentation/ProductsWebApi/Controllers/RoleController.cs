using Application.Common.Queries.Roles.GetRole;
using Application.Common.Queries.Roles.GetRoleList;
using Application.Dtos.Roles;
using Microsoft.AspNetCore.Mvc;

namespace ProductsWebApi.Controllers;

[Route("api/[controller]")]
public class RoleController : BaseController
{
    /// <summary>
    ///     Get all roles
    /// </summary>
    /// <remarks>
    ///     Sample request:
    ///     GET (HOST)/api/role/all
    /// </remarks>
    /// <returns>Returns RoleListVm</returns>
    /// <response code="200">Success</response>
    [HttpGet("all")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<RoleListVm>> GetAll()
    {
        var query = new GetAllRoleQuery();

        var vm = await Mediator.Send(query);
        return Ok(vm);
    }

    /// <summary>
    ///     Get info role by id
    /// </summary>
    /// <remarks>
    ///     Sample request:
    ///     GET (HOST)/api/role/AB670EFA-9049-46F2-AA3F-8C5044657851
    /// </remarks>
    /// <param name="id">Role id guid</param>
    /// <returns>Returns RoleLookupDto</returns>
    /// <response code="200">Success</response>
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<RoleLookupDto>> Get(Guid id)
    {
        var query = new GetDetailsRoleQuery
        {
            Id = id
        };
        var vm = await Mediator.Send(query);
        return Ok(vm);
    }
}