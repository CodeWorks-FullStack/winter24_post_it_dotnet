using System.ComponentModel.DataAnnotations;

namespace post_it_dotnet.Models;

public class Album
{
  public int Id { get; set; }
  public DateTime CreatedAt { get; set; }
  public DateTime UpdatedAt { get; set; }
  public string CreatorId { get; set; }
  [MinLength(3), MaxLength(25)] public string Title { get; set; }
  [MinLength(15), MaxLength(250)] public string Description { get; set; }
  [Url, MaxLength(3000)] public string CoverImg { get; set; }
  public bool Archived { get; set; }
  public string Category { get; set; }
}