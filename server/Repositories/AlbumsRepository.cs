
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

    Album album = _db.Query(sql, (Album album, Account account) =>
    {
      album.Creator = account;
      return album;
    }, albumData).SingleOrDefault();

    return album;
  }
}