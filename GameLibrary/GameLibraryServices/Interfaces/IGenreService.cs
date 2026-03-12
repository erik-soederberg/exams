using GameLibraryServices.DTOs.Genre;
namespace GameLibraryServices.Interfaces;

public interface IGenreService
{
    
    Task<List<GenreResponseDto>> GetAllGenresAsync();
    Task<GenreResponseDto?> GetByIdAsync(int id);
    Task<GenreResponseDto> CreateGenreAsync(GenreRequestDto dto);
    Task<GenreResponseDto?>UpdateAsync(int id, GenreRequestDto dto);
    Task<bool> DeleteGenreAsync(int id);
    
}