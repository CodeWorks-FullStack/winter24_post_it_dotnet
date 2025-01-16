



namespace post_it_dotnet.Repositories;

public class AlbumsRepository
{
  public AlbumsRepository(IDbConnection db)
  {
    _db = db;
  }


  private readonly IDbConnection _db;

  internal Album CreateAlbum(Album albumData)
  {
    string sql = @"
    INSERT INTO
    albums(title, description, category, cover_img, creator_id)
    VALUES(@Title, @Description, @Category, @CoverImg, @CreatorId);
    
    SELECT
    albums.*,
    accounts.*
    FROM albums
    JOIN accounts ON albums.creator_id = accounts.id
    WHERE albums.id = LAST_INSERT_ID();";

    Album album = _db.Query(sql, (Album album, Profile account) =>
    {
      album.Creator = account;
      return album;
    }, albumData).SingleOrDefault();

    return album;
  }

  internal List<Album> GetAllAlbums()
  {
    string sql = @"
    SELECT
    albums.*,
    accounts.*
    FROM albums
    JOIN accounts ON albums.creator_id = accounts.id;";

    List<Album> albums = _db.Query(sql, (Album album, Profile account) =>
    {
      album.Creator = account;
      return album;
    }).ToList();
    return albums;
  }

  internal Album GetAlbumById(int albumId)
  {
    string sql = @"
    SELECT
    albums.*,
    accounts.*
    FROM albums
    JOIN accounts ON albums.creator_id = accounts.id
    WHERE albums.id = @albumId;";

    Album album = _db.Query(sql, (Album album, Profile account) =>
    {
      album.Creator = account;
      return album;
    }, new { albumId }).SingleOrDefault();

    return album;
  }

  internal void ArchiveAlbum(int albumId)
  {
    string sql = "UPDATE albums SET archived = true WHERE id = @albumId;";

    int rowsAffected = _db.Execute(sql, new { albumId });

    switch (rowsAffected)
    {
      case 1: return;

      case 0: throw new Exception("NO ROWS UPDATED");

      default: throw new Exception($"{rowsAffected} ROWS WERE UPDATED AND THAT IS BAD");
    }
  }
}