using Business.Abstract;
using Entities.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthsController : ControllerBase
{
    private IAuthService _authService;

    public AuthsController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("login")]
    public ActionResult Login(UserForLoginDto userForLoginDto, bool withDeleted)
    {
        var userToLogin = _authService.Login(userForLoginDto);
        if (!userToLogin.Success)
        {
            return BadRequest(userToLogin.Message);
        }

        var result = _authService.CreateAccessToken(userToLogin.Data, withDeleted);
        if (result.Success)
        {
            return Ok(result.Data);
        }

        return BadRequest(result.Message);
    }

    [HttpPost("register")]
    public ActionResult Register(UserForRegisterDto userForRegisterDto)
    {
        var userExists = _authService.UserExists(userForRegisterDto.Email, userForRegisterDto.UserName);
        if (!userExists.Success)
        {
            return BadRequest(userExists.Message);
        }

        var registerResult = _authService.Register(userForRegisterDto);
        if (registerResult.Success)
        {
            var result = _authService.CreateAccessToken(registerResult.Data);
            if (result.Success)
            {
                return Ok(result.Data);
            }

            return BadRequest(result.Message);
        }

        return BadRequest(registerResult.Message);
    }
}