using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using GameLibraryServices.DTOs.Auth;

namespace GameLibraryAPI.Controllers;


[ApiController]
[Route("api/[controller]")]

public class AuthController : ControllerBase
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly IConfiguration _config;

    public AuthController(
        UserManager<IdentityUser> userManager,
        RoleManager<IdentityRole> roleManager,
        IConfiguration config)

    {
        _userManager = userManager;
        _roleManager = roleManager;
        _config = config;

    }
    
    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterDto dto)
    {
        var user = new IdentityUser
        {
            UserName = dto.Username,
            Email = dto.Email
        };

        var result = await _userManager.CreateAsync(user, dto.Password);

        if (!result.Succeeded)
            return BadRequest(result.Errors);

        return Ok("User registered successfully");
    }


    
    
        
}

