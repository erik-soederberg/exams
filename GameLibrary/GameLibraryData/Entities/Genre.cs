namespace GameLibraryData.Entities;

public class Genre
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; }
    
    // Creates a relation between Genre and Games 
    public ICollection<Game> Games { get; set; } = new List<Game>();
}