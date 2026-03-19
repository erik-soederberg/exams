using GameLibraryData;
using GameLibraryData.Entities;
using GameLibraryServices.DTOs.Genre;
using GameLibraryServices.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GameLibraryServices.Services;

public class GenreService : IGenreService
{
    private readonly AppDbContext _dbContext;
    
    public GenreService(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<GenreResponseDto>> GetAllGenresAsync()
    {
        var genres = await _dbContext.Genres.ToListAsync();

        var result = genres.Select(g => new GenreResponseDto
        {
            Id = g.Id,
            Name = g.Name,
            Description = g.Description
        });
        
        return result.ToList();
    }

    public async Task<GenreResponseDto?> GetGenreAsync(int id)
    {
        var genre = await _dbContext.Genres.FindAsync(id);

        if (genre == null)
        {
            return null;
        }

        return new GenreResponseDto
        {
            Id = genre.Id,
            Name = genre.Name,
            Description = genre.Description
        };
    }

    public async Task<GenreResponseDto> CreateGenreAsync(GenreRequestDto genreRequestDto)
    {
        var newGenre = new Genre
        {
            Name = genreRequestDto.Name,
            Description = genreRequestDto.Description
        };
        
        _dbContext.Genres.Add(newGenre);

        await _dbContext.SaveChangesAsync();

        return new GenreResponseDto()
        {
            Id = newGenre.Id,
            Name = newGenre.Name,
            Description = newGenre.Description
        };
    }

    public async Task<GenreResponseDto?> UpdateGenreAsync(int id, GenreRequestDto genreRequestDto)
    {
        var genre = await _dbContext.Genres.FindAsync(id);

        if (genre == null)
        {
            return null;
        }
        
        genre.Name = genreRequestDto.Name;
        genre.Description = genreRequestDto.Description;
        
        await _dbContext.SaveChangesAsync();
        return new GenreResponseDto {
            Id = genre.Id,
            Name = genre.Name,
            Description = genre.Description
        };
    
   }
    
    public async Task<bool> DeleteGenreAsync(int id) 
    {
        var genre = await  _dbContext.Genres.FindAsync(id);

        if (genre == null)
        {
            return false;
        }
        
        _dbContext.Genres.Remove(genre);
        await _dbContext.SaveChangesAsync();
        
        return true;
        
    }
    
    
}
    
    
    
    


