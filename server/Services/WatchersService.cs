






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

  internal List<WatcherAlbum> GetWatcherAlbumsByAccountId(string userId)
  {
    List<WatcherAlbum> watcherAlbums = _repository.GetWatcherAlbumsByAccountId(userId);
    return watcherAlbums;
  }

  internal List<WatcherProfile> GetWatcherProfilesByAlbumId(int albumId)
  {
    List<WatcherProfile> watchers = _repository.GetWatcherProfilesByAlbumId(albumId);
    return watchers;
  }
}
