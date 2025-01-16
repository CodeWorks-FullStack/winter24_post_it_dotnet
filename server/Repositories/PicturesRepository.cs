



namespace post_it_dotnet.Repositories;

public class PicturesRepository
{
  public PicturesRepository(IDbConnection db)
  {
    _db = db;
  }
  private readonly IDbConnection _db;

  internal Picture CreatePicture(Picture pictureData)
  {
    string sql = @"
    INSERT INTO
    pictures(img_url, album_id, creator_id)
    VALUES(@ImgUrl, @AlbumId, @CreatorId);

    SELECT
    pictures.*,
    accounts.*
    FROM pictures
    JOIN accounts ON accounts.id = pictures.creator_id
    WHERE pictures.id = LAST_INSERT_ID();";

    Picture picture = _db.Query(sql, (Picture picture, Profile account) =>
    {
      picture.Creator = account;
      return picture;
    }, pictureData).SingleOrDefault();
    return picture;
  }

  internal List<Picture> GetPicturesByAlbumId(int albumId)
  {
    string sql = @"
    SELECT
    pictures.*,
    accounts.*
    FROM pictures
    JOIN accounts ON accounts.id = pictures.creator_id
    WHERE album_id = @albumId;";

    List<Picture> pictures = _db.Query(sql, (Picture picture, Profile account) =>
    {
      picture.Creator = account;
      return picture;
    }, new { albumId }).ToList();

    return pictures;
  }

  internal Picture GetPictureById(int pictureId)
  {
    string sql = "SELECT * FROM pictures WHERE id = @pictureId;";

    Picture picture = _db.Query<Picture>(sql, new { pictureId }).SingleOrDefault();

    return picture;
  }

  internal void DeletePicture(int pictureId)
  {
    string sql = "DELETE FROM pictures WHERE id = @pictureId LIMIT 1;";

    int rowsAffected = _db.Execute(sql, new { pictureId });

    switch (rowsAffected)
    {
      case 1: return;

      case 0: throw new Exception("NO ROWS UPDATED");

      default: throw new Exception($"{rowsAffected} ROWS WERE UPDATED AND THAT IS BAD");
    }
  }
}