using dotnet_chat.Data;
using dotnet_chat.DTOs;
using dotnet_chat.Helpers;
using dotnet_chat.Models;
using Microsoft.AspNetCore.Mvc;

namespace dotnet_chat.Controllers;

[Route("api")]
[ApiController]
public class AuthController : Controller {
    private readonly IUserRepository _repository;
    private readonly JwtService _jwtService;

    public AuthController(IUserRepository repository, JwtService jwtService) {
        _repository = repository;
        _jwtService = jwtService;
    }

    [HttpPost("register")]
    public IActionResult Register(RegisterDTO registerDto) {
        var user = new User {
            Name = registerDto.Name,
            Email = registerDto.Email,
            Password = BCrypt.Net.BCrypt.HashPassword(registerDto.Password)
        };

        return Created("Created!", _repository.Create(user));
    }

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

    [HttpGet("user")]
    public IActionResult GetUser() {

        try {
            var jwt = Request.Cookies["jwt"];
            var token = _jwtService.Verify(jwt);
        
            int userId = int.Parse(token.Issuer);
            var user = _repository.GetById(userId);
        
            return Ok(user);
        } catch (Exception e) {
            return Unauthorized("Log in first!");
        }
        
    }
    
    [HttpPost("logout")]
    public IActionResult Logout() {
        Response.Cookies.Delete("jwt");
        return Ok("Logged out");
    }
}