using GameLibraryServices.DTOs.Platform;
using GameLibraryServices.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GameLibraryAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PlatformController : ControllerBase
{
    private readonly IPlatformService _platformService;

    public PlatformController(IPlatformService platformService)
    {
        _platformService = platformService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllPlatforms()
    {
        var platforms = await _platformService.GetAllPlatformsAsync();
        return Ok(platforms);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetPlatform(int id)
    {
        var platform = await _platformService.GetPlatformByIdAsync(id);
        if (platform == null) return NotFound();
        return Ok(platform);
    }

    [HttpPost]
    public async Task<IActionResult> CreatePlatform(PlatformRequestDto dto)
    {
        var platform = await _platformService.CreatePlatformAsync(dto);
        return CreatedAtAction(nameof(GetPlatform), new { id = platform.Id }, platform);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdatePlatform(int id, PlatformRequestDto dto)
    {
        var platform = await _platformService.UpdatePlatformAsync(id, dto);
        if (platform == null) return NotFound();
        return Ok(platform);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeletePlatform(int id)
    {
        var result = await _platformService.DeletePlatformAsync(id);
        if (!result) return NotFound();
        return NoContent();
    }
}