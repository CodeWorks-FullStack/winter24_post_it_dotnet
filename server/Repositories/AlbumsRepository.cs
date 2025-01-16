
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
    
    SELECT * FROM albums WHERE id = LAST_INSERT_ID();";

    Album album = _db.Query<Album>(sql, albumData).SingleOrDefault();

    return album;
  }
}