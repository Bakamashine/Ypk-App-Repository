using Application.Commands.Users.CreateUser;
using Application.Commands.Users.DeleteUser;
using Application.Commands.Users.UpdateUser;
using Application.Dtos.Users;
using Application.Queries.Base;
using Application.Queries.Users.GetUser;
using Application.Queries.Users.GetUserList;
using Application.Queries.Users.GetUserPagList;
using AutoMapper;
using CourseWebApi.Models.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ProductsWebApi.Controllers;

[Route("api/[controller]")]
public class UserController : BaseController
{
    private readonly IMapper mapper;

    public UserController(IMapper mapper)
    {
        this.mapper = mapper;
    }

    /// <summary>
    ///     Get all users
    /// </summary>
    /// <remarks>
    ///     Sample request:
    ///     GET (HOST)/api/user/all
    /// </remarks>
    /// <returns>Returns UsersListVm</returns>
    /// <response code="200">Success</response>
    /// <response code="401">If the user is unauthorized</response>
    /// <response code="403">If the user not have permission</response>
    [Authorize(Roles = "Admin")]
    [HttpGet("all")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<ActionResult<UserListVm>> GetAll()
    {
        var query = new GetAllUserQuery();

        var vm = await Mediator.Send(query);

        if (vm?.Users != null)
            foreach (var product in vm.Users)
                if (!string.IsNullOrEmpty(product.AvatarPath))
                    product.AvatarUrl = $"{Request.Scheme}://{Request.Host}{product.AvatarPath}";

        return Ok(vm);
    }

    [Authorize(Roles = "Admin")]
    [HttpGet("pag/all")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<ActionResult<PagedList<UserLookupDto>>> GetAllWithPag(
        [FromQuery] int pageSize = 5,
        [FromQuery] int page = 1
    )
    {
        var query = new GetAllUserPagQuery { PageSize = pageSize, Page = page };

        var vm = await Mediator.Send(query);

        if (vm?.Items != null)
            foreach (var product in vm.Items)
                if (!string.IsNullOrEmpty(product.AvatarPath))
                    product.AvatarUrl = $"{Request.Scheme}://{Request.Host}{product.AvatarPath}";

        return Ok(vm);
    }

    /// <summary>
    ///     Get info user by id
    /// </summary>
    /// <remarks>
    ///     Sample request:
    ///     GET (HOST)/api/user/AB670EFA-9049-46F2-AA3F-8C5044657851
    /// </remarks>
    /// <param name="id">User id guid</param>
    /// <returns>Returns UserLookupDto</returns>
    /// <response code="200">Success</response>
    /// <response code="401">If the user is unauthorized</response>
    [Authorize]
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<UserLookupDto>> Get(Guid id)
    {
        var query = new GetDetailsUserQuery
        {
            Id = id
        };
        var vm = await Mediator.Send(query);
        if (!string.IsNullOrEmpty(vm.AvatarPath)) vm.AvatarUrl = $"{Request.Scheme}://{Request.Host}{vm.AvatarPath}";
        return Ok(vm);
    }

    /// <summary>
    ///     Create object user
    /// </summary>
    /// <remarks>
    ///     Sample request:
    ///     POST (HOST)/api/user
    ///     {
    ///     "fullname": "string",
    ///     "password": "string",
    ///     "phoneNumber": "string",
    ///     "userInfo": "string",
    ///     "avatar": "file",
    ///     "roleId": "7ab72f64-5717-4562-bgfc-2c963f663sa2",
    ///     }
    /// </remarks>
    /// <param name="createUserDto">CreateUserDto object</param>
    /// <returns>Returns id (guid)</returns>
    /// <response code="201">Success</response>
    /// <response code="401">If the user is unauthorized</response>
    /// <response code="403">If the user not have permission</response>
    [Authorize(Roles = "Admin")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [HttpPost]
    public async Task<ActionResult<Guid>> Create([FromForm] CreateUserDto createUserDto)
    {
        var command = mapper.Map<CreateUserCommand>(createUserDto);
        command.CurrentUserId = UserId;
        var commandId = await Mediator.Send(command);
        return Ok(commandId);
    }

    /// <summary>
    ///     Update object user
    /// </summary>
    /// <remarks>
    ///     Sample request:
    ///     PUT (HOST)/api/user
    ///     {
    ///     "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
    ///     "fullname": "string",
    ///     "OldPassword": "string",
    ///     "NewPassword": "string",
    ///     "phoneNumber": "string",
    ///     "userInfo": "string",
    ///     "avatar": "file",
    ///     "IsActive": "bool",
    ///     }
    /// </remarks>
    /// <param name="updateUserDto">UpdateUserDto object</param>
    /// <returns>Returns NoContent</returns>
    /// <response code="204">Success</response>
    /// <response code="401">If the user is unauthorized</response>
    [Authorize]
    [HttpPut]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> Update([FromForm] UpdateUserDto updateUserDto)
    {
        var command = mapper.Map<UpdateUserCommand>(updateUserDto);
        command.CurrentUserId = UserId;
        await Mediator.Send(command);
        return NoContent();
    }

    /// <summary>
    ///     Update object user for admin
    /// </summary>
    /// <remarks>
    ///     Sample request:
    ///     PUT (HOST)/api/user/admin
    ///     {
    ///     "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
    ///     "fullname": "string",
    ///     "phoneNumber": "string",
    ///     "userInfo": "string",
    ///     "avatar": "file",
    ///     "roleId": "7ab72f64-5717-4562-bgfc-2c963f663sa2",
    ///     "YpkId": "76f24f64-5717-4562-bgfc-2c963f663sa2",
    ///     }
    /// </remarks>
    /// <param name="updateUserDto">UpdateUserForAdminDto object</param>
    /// <returns>Returns NoContent</returns>
    /// <response code="204">Success</response>
    /// <response code="401">If the user is unauthorized</response>
    /// <response code="403">If the user not have permission</response>
    [Authorize(Roles = "Admin")]
    [HttpPut("admin")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<IActionResult> UpdateAdmin([FromForm] UpdateUserForAdminDto updateUserDto)
    {
        var command = mapper.Map<UpdateAdminUserCommand>(updateUserDto);
        command.CurrentUserId = UserId;
        await Mediator.Send(command);
        return NoContent();
    }


    /// <summary>
    ///     Delete object user by id
    /// </summary>
    /// <remarks>
    ///     Sample request:
    ///     DELETE (HOST)/api/user/AB670EFA-9049-46F2-A5BF-8C5044287851
    /// </remarks>
    /// <param name="id">User id (guid)</param>
    /// <returns>Returns NoContent</returns>
    /// <response code="204">Success</response>
    /// <response code="401">If the user is unauthorized</response>
    [Authorize]
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
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