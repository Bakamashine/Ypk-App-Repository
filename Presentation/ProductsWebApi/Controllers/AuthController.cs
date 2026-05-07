using System.Security.Claims;
using Application.Commands.Auth.Login;
using Application.Commands.Auth.Registration;
using Application.Dtos.Auth;
using Application.Interfaces;
using Application.Interfaces.Repository;
using AutoMapper;
using CourseWebApi.Models.Auth;
using Microsoft.AspNetCore.Authorization;
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
        try
        {
            var command = mapper.Map<LoginUserCommand>(loginDto);

            var response = await Mediator.Send(command);
            if (response is null)
                return Unauthorized();

            // return Ok(new { accessToken = response.AccessToken });
            return Ok(TokenDto.Create(response.AccessToken, response.RefreshToken));
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            return StatusCode(500);
        }

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
    ///     "refreshToken": "string"
    ///     }
    /// </remarks>
    /// <param name="request">LoginViaToken object</param>
    /// <returns>Returns access token</returns>
    /// <response code="200">Success</response>
    [HttpPost("loginViaToken")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> LoginViaRefreshToken([FromBody] LoginViaRefreshTokenRequest? request = null)
    {
        string refreshToken;
        if (request != null && !string.IsNullOrEmpty(request.refreshToken))
        {
            refreshToken = request.refreshToken;
        }
        else
        {
            refreshToken = HttpContext.Request.Cookies["refreshToken"] ?? string.Empty;
        }

        if (string.IsNullOrEmpty(refreshToken)) return Unauthorized();
        var user = await _userRepository.GetByRefreshToken(refreshToken);
        if (user == null) return Unauthorized();
        await _service.InvalidateRefreshTokenAsync(refreshToken);

        var accessToken = await _service.GenerateJwtToken(user);
        var newRefreshToken = await _service.GenerateRefreshToken(user);

        return Ok(TokenDto.Create(accessToken, newRefreshToken));

    }


    /// <summary>
    ///     Account logout
    /// </summary>
    /// <remarks>
    ///     Sample request:
    ///     POST (HOST)/api/Auth/logout
    ///     {
    ///     "refreshToken": "string"
    ///     }
    /// </remarks>
    /// <param name="request">LogoutRequest object</param>
    /// <returns></returns>
    [HttpPost("logout")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> Logout([FromBody] LogoutRequest request)
    {
        await _service.InvalidateRefreshTokenAsync(request.refreshToken);
        return Ok();
    }

    [HttpGet("test")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [Authorize]
    public async Task<string> Test()
    {
        return "It's working!";
    }

    /// <summary>
    /// Get info about user by token
    /// </summary>
    /// <returns></returns>
    [HttpGet("me")]
    [Authorize]
    public IActionResult GetMe()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var name = User.FindFirstValue(ClaimTypes.Name);
        var phone = User.FindFirstValue(ClaimTypes.MobilePhone);
        var role = User.FindFirstValue(ClaimTypes.Role);
        if (userId == null)
            return Unauthorized();
        return Ok(new
        {
            id = userId,
            name = name,
            phoneNumber = phone,
            role = role
        });
    }
}