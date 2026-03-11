namespace GameLibraryAPI.DTOs.Game;

public class GameRequestDto
{
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public int ReleaseYear { get; set; }
    public double Rating { get; set; }
    
    public List<int> GenreId { get; set; } = new List<int>();
    public List<int> PlatformId { get; set; } = new List<int>();
}