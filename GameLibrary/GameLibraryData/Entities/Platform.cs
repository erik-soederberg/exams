namespace GameLibraryData.Entities;

public class Platform
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Manufacturer { get; set; }

    // Navigation property
    public ICollection<GamePlatform> GamePlatforms { get; set; } = new List<GamePlatform>();
}