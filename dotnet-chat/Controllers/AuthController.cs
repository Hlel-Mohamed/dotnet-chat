using dotnet_chat.Data;
using dotnet_chat.DTOs;
using dotnet_chat.Helpers;
using dotnet_chat.Models;
using Microsoft.AspNetCore.Mvc;

namespace dotnet_chat.Controllers;

/// <summary>
///     AuthController is a controller class that handles authentication related requests.
/// </summary>
[Route("api")]
[ApiController]
public class AuthController : Controller
{
    private readonly IUserRepository _repository;
    private readonly JwtService _jwtService;

    /// <summary>
    ///     The constructor for the AuthController class.
    /// </summary>
    /// <param name="repository">An instance of IUserRepository to interact with the user data.</param>
    /// <param name="jwtService">An instance of JwtService to handle JWT related operations.</param>
    public AuthController(IUserRepository repository, JwtService jwtService) {
        _repository = repository;
        _jwtService = jwtService;
    }

    /// <summary>
    ///     Register is an action method that handles HTTP POST requests for user registration.
    /// </summary>
    /// <param name="registerDto">The data transfer object containing the registration details.</param>
    /// <returns>An IActionResult that represents the result of the registration process.</returns>
    [HttpPost("register")]
    public IActionResult Register(RegisterDTO registerDto) {
        var user = new User {
            Name = registerDto.Name,
            Email = registerDto.Email,
            Password = BCrypt.Net.BCrypt.HashPassword(registerDto.Password)
        };

        return Created("Created!", _repository.Create(user));
    }

    /// <summary>
    ///     Login is an action method that handles HTTP POST requests for user login.
    /// </summary>
    /// <param name="loginDto">The data transfer object containing the login details.</param>
    /// <returns>An IActionResult that represents the result of the login process.</returns>
    [HttpPost("login")]
    public IActionResult Login(LoginDTO loginDto) {
        var user = _repository.GetByEmail(loginDto.Email);

        if (user == null) {
            return NotFound("Email not found");
        }

        if (!BCrypt.Net.BCrypt.Verify(loginDto.Password, user.Password)) {
            return NotFound("Invalid password");
        }

        var jwt = _jwtService.Generate(user.Id);

        Response.Cookies.Append("jwt", jwt, new CookieOptions {
            HttpOnly = true
        });
        return Ok();
    }

    /// <summary>
    ///     GetUser is an action method that handles HTTP GET requests to retrieve the current user.
    /// </summary>
    /// <returns>An IActionResult that represents the result of the get user process.</returns>
    [HttpGet("user")]
    public IActionResult GetUser() {
        try {
            var jwt = Request.Cookies["jwt"];
            var token = _jwtService.Verify(jwt);

            var userId = int.Parse(token.Issuer);
            var user = _repository.GetById(userId);

            return Ok(user);
        } catch (Exception e) {
            return Unauthorized("Log in first!");
        }
    }

    /// <summary>
    ///     Logout is an action method that handles HTTP POST requests for user logout.
    /// </summary>
    /// <returns>An IActionResult that represents the result of the logout process.</returns>
    [HttpPost("logout")]
    public IActionResult Logout() {
        Response.Cookies.Delete("jwt");
        return Ok("Logged out");
    }
}