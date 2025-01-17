






namespace post_it_dotnet.Repositories;

public class WatchersRepository
{
  private readonly IDbConnection _db;

  public WatchersRepository(IDbConnection db)
  {
    _db = db;
  }

  internal Watcher CreateWatcher(Watcher watcherData)
  {
    string sql = @"
    INSERT INTO 
    watchers(album_id, account_id)
    VALUES(@AlbumId, @AccountId);

    SELECT * FROM watchers WHERE id = LAST_INSERT_ID();";

    Watcher watcher = _db.Query<Watcher>(sql, watcherData).SingleOrDefault();

    return watcher;
  }

  internal List<WatcherAlbum> GetWatcherAlbumsByAccountId(string userId)
  {
    string sql = @"
    SELECT
    watchers.*,
    albums.*,
    accounts.*
    FROM watchers
    JOIN albums ON watchers.album_id = albums.id
    JOIN accounts ON accounts.id = albums.creator_id
    WHERE watchers.account_id = @userId";

    List<WatcherAlbum> watcherAlbums = _db.Query(sql, (Watcher watcher, WatcherAlbum album, Profile account) =>
    {
      album.AccountId = watcher.AccountId;
      album.WatcherId = watcher.Id;
      album.Creator = account;
      return album;
    }, new { userId }).ToList();

    return watcherAlbums;
  }

  internal List<WatcherProfile> GetWatcherProfilesByAlbumId(int albumId)
  {
    string sql = @"
    SELECT
    watchers.*,
    accounts.*
    FROM watchers
    JOIN accounts ON watchers.account_id = accounts.id
    WHERE album_id = @albumId;";

    List<WatcherProfile> watcherProfiles = _db.Query(sql, (Watcher watcher, WatcherProfile account) =>
    {
      account.AlbumId = watcher.AlbumId;
      account.WatcherId = watcher.Id; // id of the many-to-many
      return account;
    }, new { albumId }).ToList();

    return watcherProfiles;
  }

}
