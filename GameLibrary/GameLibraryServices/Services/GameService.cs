using GameLibraryData;
using GameLibraryData.Entities;
using GameLibraryServices.DTOs.Game;
using GameLibraryServices.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GameLibraryServices.Services;

public class GameService : IGameService
{
    private readonly AppDbContext _dbContext;

    public GameService(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<GameResponseDto>> GetAllGamesAsync()
    {
        var games = await _dbContext.Games
            .Include(g => g.GameGenres).ThenInclude(gg => gg.Genre)
            .Include(g => g.GamePlatforms).ThenInclude(gp => gp.Platform)
            .ToListAsync();

        return games.Select(ToResponseDto).ToList();
    }

    public async Task<GameResponseDto?> GetByIdAsync(int id)
    {
        var game = await _dbContext.Games
            .Include(g => g.GameGenres).ThenInclude(gg => gg.Genre)
            .Include(g => g.GamePlatforms).ThenInclude(gp => gp.Platform)
            .FirstOrDefaultAsync(g => g.Id == id);

        if (game == null) return null;

        return ToResponseDto(game);
    }

    public async Task<GameResponseDto> CreateGameAsync(GameRequestDto dto)
    {
        var game = new Game
        {
            Title = dto.Title,
            Description = dto.Description,
            ReleaseYear = dto.ReleaseYear,
            Rating = dto.Rating,
            GameGenres = dto.GenreId.Select(id => new GameGenre { GenreId = id }).ToList(),
            GamePlatforms = dto.PlatformId.Select(id => new GamePlatform { PlatformId = id }).ToList()
        };

        _dbContext.Games.Add(game);
        await _dbContext.SaveChangesAsync();

        return await GetByIdAsync(game.Id) ?? ToResponseDto(game);
    }

    public async Task<GameResponseDto?> UpdateAsync(int id, GameRequestDto dto)
    {
        var game = await _dbContext.Games
            .Include(g => g.GameGenres)
            .Include(g => g.GamePlatforms)
            .FirstOrDefaultAsync(g => g.Id == id);

        if (game == null) return null;

        game.Title = dto.Title;
        game.Description = dto.Description;
        game.ReleaseYear = dto.ReleaseYear;
        game.Rating = dto.Rating;

        game.GameGenres.Clear();
        foreach (var genreId in dto.GenreId)
            game.GameGenres.Add(new GameGenre { GameId = id, GenreId = genreId });

        game.GamePlatforms.Clear();
        foreach (var platformId in dto.PlatformId)
            game.GamePlatforms.Add(new GamePlatform { GameId = id, PlatformId = platformId });

        await _dbContext.SaveChangesAsync();

        return await GetByIdAsync(id);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var game = await _dbContext.Games.FindAsync(id);

        if (game == null) return false;

        _dbContext.Games.Remove(game);
        await _dbContext.SaveChangesAsync();

        return true;
    }

    private static GameResponseDto ToResponseDto(Game game) => new()
    {
        Id = game.Id,
        Title = game.Title,
        Description = game.Description ?? string.Empty,
        ReleaseYear = game.ReleaseYear,
        Rating = game.Rating,
        Genres = game.GameGenres.Select(gg => gg.Genre?.Name ?? string.Empty).ToList(),
        Platforms = game.GamePlatforms.Select(gp => gp.Platform?.Name ?? string.Empty).ToList()
    };
}
