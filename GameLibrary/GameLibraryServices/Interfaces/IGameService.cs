using GameLibraryServices.DTOs.Game;
namespace GameLibraryServices.Interfaces;

public interface IGameService
{

    Task<List<GameResponseDto>> GetAllGamesAsync();
    Task<GameResponseDto?> GetByIdAsync(int id);
    Task<GameResponseDto> CreateGameAsync(GameRequestDto dto);
    Task<GameResponseDto?>UpdateAsync(int id, GameRequestDto dto);
    Task<bool> DeleteAsync(int id);

}