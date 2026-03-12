using GameLibraryServices.DTOs.Genre;
namespace GameLibraryServices.Interfaces;

public interface IGenreService
{
    
    Task<List<GenreResponseDto>> GetAllGenresAsync();
    Task<GenreResponseDto?> GetGenreAsync(int id);
    Task<GenreResponseDto> CreateGenreAsync(GenreRequestDto dto);
    Task<GenreResponseDto?>UpdateGenreAsync(int id, GenreRequestDto dto);
    Task<bool> DeleteGenreAsync(int id);
    
}