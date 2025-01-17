








namespace post_it_dotnet.Repositories;

public class WatchersRepository
{
  private readonly IDbConnection _db;

  public WatchersRepository(IDbConnection db)
  {
    _db = db;
  }

  internal WatcherProfile CreateWatcher(Watcher watcherData)
  {
    string sql = @"
    INSERT INTO 
    watchers(album_id, account_id)
    VALUES(@AlbumId, @AccountId);

    SELECT
    accounts.*,
    watchers.album_id AS album_id,
    watchers.id AS watcher_id
    FROM watchers
    JOIN accounts ON watchers.account_id = accounts.id
    WHERE watchers.id = LAST_INSERT_ID();";

    WatcherProfile watcherProfile = _db.Query<WatcherProfile>(sql, watcherData).SingleOrDefault();

    return watcherProfile;
  }

  internal void DeleteWatcher(int watcherId)
  {
    string sql = "DELETE FROM watchers WHERE id = @watcherId;";

    int rowsAffected = _db.Execute(sql, new { watcherId });

    switch (rowsAffected)
    {
      case 1: return;

      case 0: throw new Exception("NO ROWS UPDATED");

      default: throw new Exception($"{rowsAffected} ROWS WERE UPDATED AND THAT IS BAD");
    }
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

  internal Watcher GetWatcherById(int watcherId)
  {
    string sql = "SELECT * FROM watchers WHERE id = @watcherId;";

    Watcher watcher = _db.Query<Watcher>(sql, new { watcherId }).SingleOrDefault();

    return watcher;
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
