using Application.Commands.Auth.Login;
using Application.Commands.Auth.Registration;
using Application.Dtos.Auth;
using Application.Interfaces;
using Application.Interfaces.Repository;
using AutoMapper;
using CourseWebApi.Models.Auth;
using Microsoft.AspNetCore.Mvc;
using ProductsWebApi.Controllers;

namespace CourseWebApi.Controllers;

[Route("api/[controller]")]
public class AuthController : BaseController
{
    private readonly IJwtTokenService _service;
    private readonly IUserRepository _userRepository;
    private readonly IMapper mapper;

    public AuthController(IMapper mapper, IJwtTokenService service, IUserRepository userRepository)
    {
        this.mapper = mapper;
        _service = service;
        _userRepository = userRepository;
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

        // return Ok(new { accessToken = response.AccessToken });
        return Ok(TokenDto.Create(response.AccessToken, response.RefreshToken));
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

        
        var accessToken = response.AccessToken;
        var refreshToken = response.RefreshToken;
        return Ok(TokenDto.Create(accessToken, refreshToken));
        // return Ok(new { accessToken = response.AccessToken });
    }

    /// <summary>
    ///     Account registration
    /// </summary>
    /// <remarks>
    ///     Sample request:
    ///     POST (HOST)/api/Auth/register
    ///     {
    ///     "rememberToken": "string"
    ///     }
    /// </remarks>
    /// <param name="loginViaToken">LoginViaToken object</param>
    /// <returns>Returns access token</returns>
    /// <response code="200">Success</response>
    [HttpPost("loginViaToken")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> LoginViaRememberToken([FromBody] LoginViaTokenRequest request)
    {
        if (await _service.ValidateRefreshTokenAsync(request.refreshToken))
        {
            var user = await _userRepository.GetByRememberToken(request.refreshToken);
            if (user == null) return Unauthorized();
            var accessToken = await _service.GenerateJwtToken(user);
            return Ok(TokenDto.Create(accessToken, request.refreshToken));
        }

        return Unauthorized();
    }
}