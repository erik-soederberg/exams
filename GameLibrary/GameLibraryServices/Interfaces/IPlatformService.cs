using GameLibraryServices.DTOs.Platform;
namespace GameLibraryServices.Interfaces;

public interface IPlatformService
{
    Task<List<PlatformResponseDto>> GetAllPlatformsAsync();
    Task<PlatformResponseDto?> GetPlatformByIdAsync(int id);
    Task<PlatformResponseDto> CreatePlatformAsync(PlatformRequestDto dto);
    Task<PlatformResponseDto?>UpdatePlatformAsync(int id, PlatformRequestDto dto);
    Task<bool> DeletePlatformAsync(int id);
}