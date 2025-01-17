namespace post_it_dotnet.Models;

// NOTE backing class for the many-to-many in the database, also going to be used for request body on a create
public class Watcher
{
  public int Id { get; set; }
  public string AccountId { get; set; }
  public int AlbumId { get; set; }
  public DateTime CreatedAt { get; set; }
  public DateTime UpdatedAt { get; set; }
}

public class WatcherProfile : Profile // all members from profile are inherited
{
  public int WatcherId { get; set; } // id of the many-to-many
  public int AlbumId { get; set; } // id of the album from the many-to-many
}

public class WatcherAlbum : Album // all members from album are inherited
{
  public int WatcherId { get; set; } // id of the many-to-many
  public string AccountId { get; set; } // if og the account from the many-to-many
}