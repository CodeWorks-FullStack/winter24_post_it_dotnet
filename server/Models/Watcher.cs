namespace post_it_dotnet.Models;

public class Watcher
{
  public int Id { get; set; }
  public string AccountId { get; set; }
  public int AlbumId { get; set; }
  public DateTime CreatedAt { get; set; }
  public DateTime UpdatedAt { get; set; }
}