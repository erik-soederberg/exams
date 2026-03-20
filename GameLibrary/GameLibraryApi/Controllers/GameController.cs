using GameLibraryServices.DTOs.Game;
using GameLibraryServices.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GameLibraryAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class GameController : ControllerBase
{
    private readonly IGameService _gameService;

    public GameController(IGameService gameService)
    {
        _gameService = gameService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllGames()
    {
        var games = await _gameService.GetAllGamesAsync();
        return Ok(games);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetGame(int id)
    {
        var game = await _gameService.GetByIdAsync(id);
        if (game == null) return NotFound();
        return Ok(game);
    }

    [HttpPost]
    public async Task<IActionResult> CreateGame(GameRequestDto dto)
    {
        var game = await _gameService.CreateGameAsync(dto);
        return CreatedAtAction(nameof(GetGame), new { id = game.Id }, game);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateGame(int id, GameRequestDto dto)
    {
        var game = await _gameService.UpdateAsync(id, dto);
        if (game == null) return NotFound();
        return Ok(game);
    }

    [Authorize]
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteGame(int id)
    {
        var result = await _gameService.DeleteAsync(id);
        if (!result) return NotFound();
        return NoContent();
    }
}