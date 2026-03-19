using GameLibraryData;
using GameLibraryData.Entities;
using GameLibraryServices.DTOs.Platform;
using GameLibraryServices.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GameLibraryServices.Services;

public class PlatformService : IPlatformService
{
    private readonly AppDbContext _dbContext;

    public PlatformService(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<PlatformResponseDto>> GetAllPlatformsAsync()
    {
        var platforms = await _dbContext.Platforms.ToListAsync();

        return platforms.Select(p => new PlatformResponseDto
        {
            Id = p.Id,
            Name = p.Name,
            Manufacturer = p.Manufacturer
        }).ToList();
    }

    public async Task<PlatformResponseDto?> GetPlatformByIdAsync(int id)
    {
        var platform = await _dbContext.Platforms.FindAsync(id);
        if (platform == null) return null;

        return new PlatformResponseDto
        {
            Id = platform.Id,
            Name = platform.Name,
            Manufacturer = platform.Manufacturer
        };
    }

    public async Task<PlatformResponseDto> CreatePlatformAsync(PlatformRequestDto dto)
    {
        var platform = new Platform
        {
            Name = dto.Name,
            Manufacturer = dto.Manufacturer
        };

        _dbContext.Platforms.Add(platform);
        await _dbContext.SaveChangesAsync();

        return new PlatformResponseDto
        {
            Id = platform.Id,
            Name = platform.Name,
            Manufacturer = platform.Manufacturer
        };
    }

    public async Task<PlatformResponseDto?> UpdatePlatformAsync(int id, PlatformRequestDto dto)
    {
        var platform = await _dbContext.Platforms.FindAsync(id);
        if (platform == null) return null;

        platform.Name = dto.Name;
        platform.Manufacturer = dto.Manufacturer;

        await _dbContext.SaveChangesAsync();

        return new PlatformResponseDto
        {
            Id = platform.Id,
            Name = platform.Name,
            Manufacturer = platform.Manufacturer
        };
    }

    public async Task<bool> DeletePlatformAsync(int id)
    {
        var platform = await _dbContext.Platforms.FindAsync(id);
        if (platform == null) return false;

        _dbContext.Platforms.Remove(platform);
        await _dbContext.SaveChangesAsync();

        return true;
    }
}