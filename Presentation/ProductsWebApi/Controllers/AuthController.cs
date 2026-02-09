

using Application.Commands.Auth.Login;
using Application.Commands.Auth.Registration;
using AutoMapper;
using CourseWebApi.Models.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProductsWebApi.Controllers;
using System.Diagnostics;
using System.Net;

namespace CourseWebApi.Controllers
{
    [Route("api/[controller]")]
    public class AuthController : BaseController
    {
        private readonly IMapper mapper;

        public AuthController(IMapper mapper)
        {
            this.mapper = mapper;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            var command = mapper.Map<LoginUserCommand>(loginDto);

            var response = await Mediator.Send(command);
            if (response is null) 
                return Unauthorized();

            return Ok(new {accessToken = response.AccessToken});
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegistrationDto registrationDto)
        {
            var command = mapper.Map<RegistrationUserCommand>(registrationDto);

            var response = await Mediator.Send(command);
            if (response is null)
                return Unauthorized();

            return Ok(new { accessToken = response.AccessToken});

        }
    }
}
