using Application.Commands.Auth.Login;
using Application.Commands.Auth.Registration;
using AutoMapper;
using CourseWebApi.Models.Auth;
using Microsoft.AspNetCore.Mvc;
using ProductsWebApi.Controllers;

namespace CourseWebApi.Controllers;

[Route("api/[controller]")]
public class AuthController : BaseController
{
    private readonly IMapper mapper;

    public AuthController(IMapper mapper)
    {
        this.mapper = mapper;
    }

    /// <summary>
    ///     Account authorization
    /// </summary>
    /// <remarks>
    ///     Sample request:
    ///     POST (HOST)/api/Auth/login
    ///     {
    ///     "login": "string",
    ///     "password": "string"
    ///     }
    /// </remarks>
    /// <param name="loginDto">LoginDto object</param>
    /// <returns>Returns access and refresh tokens</returns>
    /// <response code="200">Success</response>
    [HttpPost("login")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
    {
        var command = mapper.Map<LoginUserCommand>(loginDto);

        var response = await Mediator.Send(command);
        if (response is null)
            return Unauthorized();

        return Ok(new { accessToken = response.AccessToken });
    }

    /// <summary>
    ///     Account registration
    /// </summary>
    /// <remarks>
    ///     Sample request:
    ///     POST (HOST)/api/Auth/register
    ///     {
    ///     "nameUser": "string",
    ///     "login": "string",
    ///     "password": "string",
    ///     "email": "string",
    ///     "phoneNumber": "string"
    ///     }
    /// </remarks>
    /// <param name="registrationDto">RegistrationDto object</param>
    /// <returns>Returns access and refresh tokens</returns>
    /// <response code="200">Success</response>
    [HttpPost("register")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> Register([FromBody] RegistrationDto registrationDto)
    {
        var command = mapper.Map<RegistrationUserCommand>(registrationDto);

        var response = await Mediator.Send(command);
        if (response is null)
            return Unauthorized();

        return Ok(new { accessToken = response.AccessToken });
    }
}