using GameLibraryServices.Interfaces;
using GameLibraryServices.DTOs.Genre;
using Microsoft.AspNetCore.Mvc;

namespace GameLibraryAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class GenreController : ControllerBase
{
    private readonly IGenreService _genreService;

    public GenreController(IGenreService genreService)
    {
        _genreService = genreService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllGenres()
    {
        var genres = await _genreService.GetAllGenresAsync();
        return Ok(genres);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetGenre(int id)
    {
        var genre = await _genreService.GetGenreAsync(id);
        if (genre == null) return NotFound();
        return Ok(genre);
    }

    [HttpPost]
    public async Task<IActionResult> CreateGenre(GenreRequestDto dto)
    {
        var genre = await _genreService.CreateGenreAsync(dto);
        return CreatedAtAction(nameof(GetGenre), new { id = genre.Id }, genre);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateGenre(int id, GenreRequestDto dto)
    {
        var genre = await _genreService.UpdateGenreAsync(id, dto);
        if (genre == null) return NotFound();
        return Ok(genre);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteGenre(int id)
    {
        var result = await _genreService.DeleteGenreAsync(id);
        if (!result) return NotFound();
        return NoContent();
    }
}