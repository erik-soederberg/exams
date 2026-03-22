using Microsoft.EntityFrameworkCore;
using GameLibraryData.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace GameLibraryData;

// Inherits from IdentityDbContext to get user/role tables from ASP.NET Core Identity
public class AppDbContext : IdentityDbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Game> Games => Set<Game>();
    public DbSet<Genre> Genres => Set<Genre>();
    public DbSet<Platform> Platforms => Set<Platform>();
    public DbSet<GameGenre> GameGenres => Set<GameGenre>();
    public DbSet<GamePlatform> GamePlatforms => Set<GamePlatform>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Important: call base first so Identity can configure its own tables
        base.OnModelCreating(modelBuilder);
        
        // GameGenre and GamePlatform are join tables for many-to-many relationships
        // They need composite primary keys since they don't have their own Id
        modelBuilder.Entity<GameGenre>()
            .HasKey(gg => new { gg.GameId, gg.GenreId });

        modelBuilder.Entity<GamePlatform>()
            .HasKey(gp => new { gp.GameId, gp.PlatformId });
        
        // Seed data so the API has something to work with from the start
        modelBuilder.Entity<Genre>().HasData(
            new Genre { Id = 1, Name = "RPG", Description = "Role-playing games" },
            new Genre { Id = 2, Name = "Action", Description = "Action games" },
            new Genre { Id = 3, Name = "Strategy", Description = "Strategy games" }
        );

        modelBuilder.Entity<Platform>().HasData(
            new Platform { Id = 1, Name = "PC", Manufacturer = "Various" },
            new Platform { Id = 2, Name = "PS5", Manufacturer = "Sony" },
            new Platform { Id = 3, Name = "Xbox Series X", Manufacturer = "Microsoft" }
        );

        modelBuilder.Entity<Game>().HasData(
            new Game { Id = 1, Title = "Elden Ring", Description = "Open world RPG", ReleaseYear = 2022, Rating = 9.5 },
            new Game { Id = 2, Title = "God of War", Description = "Action adventure", ReleaseYear = 2018, Rating = 9.0 },
            new Game { Id = 3, Title = "Civilization VI", Description = "Turn-based strategy", ReleaseYear = 2016, Rating = 8.5 }
        );

        // Link games to genres and platforms through the join tables
        modelBuilder.Entity<GameGenre>().HasData(
            new GameGenre { GameId = 1, GenreId = 1 },
            new GameGenre { GameId = 2, GenreId = 2 },
            new GameGenre { GameId = 3, GenreId = 3 }
        );

        modelBuilder.Entity<GamePlatform>().HasData(
            new GamePlatform { GameId = 1, PlatformId = 1 },
            new GamePlatform { GameId = 2, PlatformId = 2 },
            new GamePlatform { GameId = 3, PlatformId = 1 }
        );
    }
}