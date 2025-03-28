using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using VideoCall.Application.Abstractions.ApiResponse;
using VideoCall.Core.Account.Requests;
using VideoCall.Core.Account.Responses;
using VideoCall.Core.Dto;
using VideoCall.Core.Entities;
using VideoCall.Core.Errors;

namespace VideoCall.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AccountController(SignInManager<User> _signInManager, UserManager<User> _userManager, IConfiguration configuration) : Controller
{

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] RegisterRequest request)
    {
        var user = await _userManager.FindByNameAsync(request.UserName);
        if (user == null)
        {
            return Unauthorized(new GenericResponse<User>(DomainErrors.UserErrors.UserWrongUserNameOrPassword));
        }

        var result = await _signInManager.CheckPasswordSignInAsync(user, request.Password, lockoutOnFailure: false);
        if (!result.Succeeded)
        {
            return Unauthorized(new GenericResponse<User>(DomainErrors.UserErrors.UserWrongUserNameOrPassword));
        }


        var token = GenerateJwtToken(request.UserName);

        return Ok(new GenericResponse<LoginResponse>(new LoginResponse
        {
            Token = token
        }));
    }  

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterRequest request)
    {
        var existingUser = await _userManager.FindByNameAsync(request.UserName);
        if (existingUser != null)
        {
            return BadRequest(new GenericResponse<User>(DomainErrors.UserErrors.UserTakenUsername));
        }

        var user = new User
        {
            UserName = request.UserName,
        };

        var result = await _userManager.CreateAsync(user, request.Password);
        if (!result.Succeeded)
        {
            return BadRequest(new GenericResponse<User>(DomainErrors.UserErrors.UserCheckPasswordValidations));
        }

        return Ok(new GenericResponse<string>("User registered successfully."));
    }

    [Authorize]
    [HttpPost("getAllUsers")]
    public async Task<IActionResult> GetAllUsers()
    {
        var users = await _userManager.Users
            .AsNoTracking()
            .Select(user => new UserDto(user.Id, user.UserName))
            .ToListAsync();

        return Ok(users);
    }

    [Authorize]
    [HttpPost("logout")]
    public async Task<IActionResult> Logout()
    {
        await _signInManager.SignOutAsync();
        return Ok(new GenericResponse<string>("Logged out"));
    }


    private string GenerateJwtToken(string username)
    {
        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, username),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]!));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: "http://localhost:44372/",
            audience: "http://localhost:4200/",
            claims: claims,
            expires: DateTime.Now.AddDays(1),
            signingCredentials: creds);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
