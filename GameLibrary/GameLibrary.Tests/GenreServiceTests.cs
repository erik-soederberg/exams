using GameLibraryData;
using GameLibraryServices.DTOs.Genre;
using GameLibraryServices.Services;
using Microsoft.EntityFrameworkCore;
using GameLibraryData.Entities;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace GameLibrary.Tests;

public class GenreServiceTests
{
    private AppDbContext GetInMemoryDbContext()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .ConfigureWarnings(w => w.Ignore(InMemoryEventId.TransactionIgnoredWarning))
            .Options;
    
        return new AppDbContext(options);
    }

    [Fact]
    public async Task GetAllGenresAsync_ReturnsAllGenres()
    {
        // Arrange
        var context = GetInMemoryDbContext();
        context.Genres.AddRange(
            new Genre { Id = 1, Name = "RPG", Description = "Role-playing games" },
            new Genre { Id = 2, Name = "Action", Description = "Action games" }
        );
        await context.SaveChangesAsync();

        var service = new GenreService(context);

        // Act
        var result = await service.GetAllGenresAsync();

        // Assert
        Assert.Equal(2, result.Count);
    }

    [Fact]
    public async Task GetGenreAsync_ReturnsNull_WhenGenreNotFound()
    {
        // Arrange
        var context = GetInMemoryDbContext();
        var service = new GenreService(context);

        // Act
        var result = await service.GetGenreAsync(999);

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task CreateGenreAsync_CreatesAndReturnsGenre()
    {
        // Arrange
        var context = GetInMemoryDbContext();
        var service = new GenreService(context);
        var dto = new GenreRequestDto { Name = "Horror", Description = "Horror games" };

        // Act
        var result = await service.CreateGenreAsync(dto);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("Horror", result.Name);
        Assert.Equal(1, context.Genres.Count());
    }
}