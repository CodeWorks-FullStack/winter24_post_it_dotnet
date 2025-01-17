







namespace post_it_dotnet.Services;

public class WatchersService
{

  private readonly WatchersRepository _repository;

  public WatchersService(WatchersRepository repository)
  {
    _repository = repository;
  }

  internal Watcher CreateWatcher(Watcher watcherData)
  {
    Watcher watcher = _repository.CreateWatcher(watcherData);
    return watcher;
  }

  internal string DeleteWatcher(int watcherId, string userId)
  {
    Watcher watcher = GetWatcherById(watcherId);

    if (watcher.AccountId != userId) throw new Exception("YOU CANNOT DELETE ANOTHER USER'S WATCHER, BRUH");

    _repository.DeleteWatcher(watcherId);

    return "No longer watching that album!";
  }

  private Watcher GetWatcherById(int watcherId)
  {
    Watcher watcher = _repository.GetWatcherById(watcherId);

    if (watcher == null) throw new Exception($"Invalid watcher id: {watcherId}");

    return watcher;
  }

  internal List<WatcherAlbum> GetWatcherAlbumsByAccountId(string userId)
  {
    List<WatcherAlbum> watcherAlbums = _repository.GetWatcherAlbumsByAccountId(userId);
    return watcherAlbums;
  }

  internal List<WatcherProfile> GetWatcherProfilesByAlbumId(int albumId)
  {
    List<WatcherProfile> watcherProfiles = _repository.GetWatcherProfilesByAlbumId(albumId);
    return watcherProfiles;
  }
}
