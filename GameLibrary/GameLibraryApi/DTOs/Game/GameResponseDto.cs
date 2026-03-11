namespace GameLibraryAPI.DTOs.Game;

public class GameResponseDto
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public int ReleaseYear { get; set; }
    public double Rating  { get; set; }
    
    public List<string> Genres { get; set; } = new List<string>();
    public List<string> Platforms { get; set; } = new List<string>();
}