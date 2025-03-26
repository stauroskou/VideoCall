using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VideoCall.Core.Entities;

namespace VideoCall.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AccountController(SignInManager<User> _signInManager, UserManager<User> _userManager) : Controller
{

    [HttpPost("login")]
    public async Task<IActionResult> Login(string username, string password)
    {
        var user = await _userManager.FindByNameAsync(username);
        if (user == null)
        {
            return Unauthorized(new { message = "Invalid username or password" });
        }

        var result = await _signInManager.PasswordSignInAsync(user, password, isPersistent: false, lockoutOnFailure: false);
        if (!result.Succeeded)
        {
            return Unauthorized(new { message = "Invalid username or password" });
        }

        return Ok(new { message = "Login successful" });
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(string username, string password)
    {
        var existingUser = await _userManager.FindByNameAsync(username);
        if (existingUser != null)
        {
            return BadRequest(new { message = "Username is already taken." });
        }

        var user = new User
        {
            UserName = username,
        };

        var result = await _userManager.CreateAsync(user, password);
        if (!result.Succeeded)
        {
            return BadRequest(new { errors = result.Errors });
        }

        return Ok(new { message = "User registered successfully." });
    }

    [Authorize]
    [HttpPost("getAllUsers")]
    public async Task<IActionResult> GetAllUsers()
    {
        var users = await _userManager.Users.ToListAsync();
        return Ok(users);
    }

    [Authorize]
    [HttpPost("logout")]
    public async Task<IActionResult> Logout()
    {
        await _signInManager.SignOutAsync();
        return Ok(new { message = "User logged out successfully" });
    }
}
